using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CreditApps.WinForms.Data;

namespace CreditApps.WinForms.Repositories
{
    public class LoanApplicationRow
    {
        public int Id { get; set; }
        public string ApplicantFullName { get; set; } = "";
        public decimal RequestedAmount { get; set; }
        public int TermMonths { get; set; }

        // число оставляем для логики смены статуса
        public int Status { get; set; }

        // текст для отображения в таблице
        public string StatusText { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    }

    public class StatusHistoryRow
    {
        public DateTime ChangedAt { get; set; }
        public string FromStatusText { get; set; } = "";
        public string ToStatusText { get; set; } = "";
        public string? Comment { get; set; }
    }

    public class LoanApplicationRepository
    {
        // ===== 1) Список заявок =====
        public List<LoanApplicationRow> GetAll()
        {
            var list = new List<LoanApplicationRow>();

            using var conn = Db.Open();
            using var cmd = new SqlCommand(@"
SELECT Id,
       ApplicantFullName,
       RequestedAmount,
       TermMonths,
       Status,
       CASE Status
           WHEN 0 THEN N'Новая'
           WHEN 1 THEN N'На проверке'
           WHEN 2 THEN N'Одобрена'
           WHEN 3 THEN N'Отказ'
           WHEN 4 THEN N'Выдана'
           WHEN 5 THEN N'Закрыта'
           ELSE N'Неизвестно'
       END AS StatusText,
       CreatedAt
FROM dbo.LoanApplications
ORDER BY CreatedAt DESC
", conn);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new LoanApplicationRow
                {
                    Id = r.GetInt32(0),
                    ApplicantFullName = r.GetString(1),
                    RequestedAmount = r.GetDecimal(2),
                    TermMonths = r.GetInt32(3),
                    Status = r.GetInt32(4),
                    StatusText = r.GetString(5),
                    CreatedAt = r.GetDateTime(6)
                });
            }

            return list;
        }

        // ===== 2) Создание заявки =====
        public void Create(string fio, string passport, decimal income, decimal amount, int termMonths, int createdByUserId)
        {
            using var conn = Db.Open();
            using var cmd = new SqlCommand(@"
INSERT INTO dbo.LoanApplications
(ApplicantFullName, PassportNumber, MonthlyIncome, RequestedAmount, TermMonths, Status, CreatedByUserId)
VALUES
(@fio, @passport, @income, @amount, @term, 0, @userId)
", conn);

            cmd.Parameters.AddWithValue("@fio", fio);
            cmd.Parameters.AddWithValue("@passport", passport);
            cmd.Parameters.AddWithValue("@income", income);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@term", termMonths);
            cmd.Parameters.AddWithValue("@userId", createdByUserId);

            cmd.ExecuteNonQuery();
        }

        // ===== 3) Смена статуса + запись истории =====
        public void ChangeStatus(int applicationId, int fromStatus, int toStatus, int userId, string? comment)
        {
            using var conn = Db.Open();
            using var tran = conn.BeginTransaction();

            try
            {
                // 1) обновляем статус заявки
                using (var cmd = new SqlCommand(@"
UPDATE dbo.LoanApplications
SET Status = @to
WHERE Id = @id
", conn, tran))
                {
                    cmd.Parameters.AddWithValue("@to", toStatus);
                    cmd.Parameters.AddWithValue("@id", applicationId);
                    cmd.ExecuteNonQuery();
                }

                // 2) пишем историю
                using (var cmd = new SqlCommand(@"
INSERT INTO dbo.StatusHistory
(LoanApplicationId, FromStatus, ToStatus, ChangedByUserId, Comment)
VALUES
(@appId, @from, @to, @userId, @comment)
", conn, tran))
                {
                    cmd.Parameters.AddWithValue("@appId", applicationId);
                    cmd.Parameters.AddWithValue("@from", fromStatus);
                    cmd.Parameters.AddWithValue("@to", toStatus);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@comment", (object?)comment ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        // ===== 4) История статусов (для кнопки История) =====
        public List<StatusHistoryRow> GetHistory(int applicationId)
        {
            var list = new List<StatusHistoryRow>();

            using var conn = Db.Open();
            using var cmd = new SqlCommand(@"
SELECT ChangedAt,
       CASE FromStatus
           WHEN 0 THEN N'Новая'
           WHEN 1 THEN N'На проверке'
           WHEN 2 THEN N'Одобрена'
           WHEN 3 THEN N'Отказ'
           WHEN 4 THEN N'Выдана'
           WHEN 5 THEN N'Закрыта'
           ELSE N'Неизвестно'
       END AS FromStatusText,
       CASE ToStatus
           WHEN 0 THEN N'Новая'
           WHEN 1 THEN N'На проверке'
           WHEN 2 THEN N'Одобрена'
           WHEN 3 THEN N'Отказ'
           WHEN 4 THEN N'Выдана'
           WHEN 5 THEN N'Закрыта'
           ELSE N'Неизвестно'
       END AS ToStatusText,
       Comment
FROM dbo.StatusHistory
WHERE LoanApplicationId = @id
ORDER BY ChangedAt DESC
", conn);

            cmd.Parameters.AddWithValue("@id", applicationId);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new StatusHistoryRow
                {
                    ChangedAt = r.GetDateTime(0),
                    FromStatusText = r.GetString(1),
                    ToStatusText = r.GetString(2),
                    Comment = r.IsDBNull(3) ? null : r.GetString(3)
                });
            }

            return list;
        }

        // ===== 5) Последний комментарий (для tooltip при наведении) =====
        public string? GetLastComment(int applicationId)
        {
            using var conn = Db.Open();
            using var cmd = new SqlCommand(@"
SELECT TOP 1 Comment
FROM dbo.StatusHistory
WHERE LoanApplicationId = @id
  AND Comment IS NOT NULL
  AND LTRIM(RTRIM(Comment)) <> ''
ORDER BY ChangedAt DESC
", conn);

            cmd.Parameters.AddWithValue("@id", applicationId);

            var res = cmd.ExecuteScalar();
            return res == null || res == DBNull.Value ? null : res.ToString();
        }
    }
}
