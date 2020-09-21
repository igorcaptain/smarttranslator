using System;
using SmartTranslator.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SmartTranslator.DictionaryRepository
{
    public class DbDictionaryRepository : IDictionaryRepository, IDisposable
    {
        private readonly SqlConnection _connection;

        public DbDictionaryRepository()
        {
            _connection = new SqlConnection(@"Database=SmartTranslator;Server=engine\sqlexpress;Integrated Security=false;User ID=ST_Admin;Password=ST_admin911");
            try
            {
                _connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<TranslateDictionary> GetAllTranslates()
        {
            //This method needs mapper
            List<TranslateDictionary> result = new List<TranslateDictionary>();
            using (SqlCommand command = new SqlCommand("[Translate].[pub_getAllStringTranslates]", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DateTime createdDate, modifiedDate;
                        while (reader.Read())
                        {
                            DateTime.TryParse(reader["CreatedDate"].ToString(), out createdDate);
                            DateTime.TryParse(reader["ModifiedDate"].ToString(), out modifiedDate);
                            result.Add(new TranslateDictionary()
                            {
                                StringID = (int)reader["StringID"],
                                VendorCode = reader["VendorCode"].ToString(),
                                OriginalString = reader["OriginalString"].ToString(),
                                OriginalNormalizedString = reader["OriginalNormalizedString"].ToString(),
                                TranslatedString = reader["TranslatedString"].ToString(),
                                TranslatedNormalizedString = reader["TranslatedNormalizedString"].ToString(),
                                DimensionSuffix = reader["DimensionSuffix"].ToString(),
                                CreatedDate = createdDate,
                                ModifiedDate = modifiedDate
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }

        public string GetTranslateString(string origString)
        {
            string result = "";
            using (SqlCommand command = new SqlCommand("[Translate].[pub_getStringTranslate]", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PrimaryString", origString);
                try
                {
                    result = command.ExecuteScalar()?.ToString();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }

        public void InsertTranslate(TranslateDictionary translateDictionary)
        {
            using(SqlCommand command = new SqlCommand("[Translate].[pub_insertStringTranslate]", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                    new SqlParameter("@VendorCode", translateDictionary.VendorCode),
                    new SqlParameter("@OriginalString", translateDictionary.OriginalString),
                    new SqlParameter("@OriginalNormalizedString", translateDictionary.OriginalNormalizedString),
                    new SqlParameter("@TranslatedString", translateDictionary.TranslatedString),
                    new SqlParameter("@TranslatedNormalizedString", translateDictionary.TranslatedNormalizedString),
                    new SqlParameter("@DimensionSuffix", translateDictionary.DimensionSuffix)
                };
                command.Parameters.AddRange(parameters);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void UpdateTranslate(TranslateDictionary translateDictionary)
        {
            using (SqlCommand command = new SqlCommand("[Translate].[pub_updateStringTranslate]", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                    new SqlParameter("@StringID", translateDictionary.StringID),
                    new SqlParameter("@VendorCode", string.IsNullOrEmpty(translateDictionary.VendorCode) ? DBNull.Value : (object)translateDictionary.VendorCode),
                    new SqlParameter("@OriginalString", string.IsNullOrEmpty(translateDictionary.OriginalString) ? DBNull.Value : (object)translateDictionary.OriginalString),
                    new SqlParameter("@OriginalNormalizedString", string.IsNullOrEmpty(translateDictionary.OriginalNormalizedString) ? DBNull.Value : (object)translateDictionary.OriginalNormalizedString),
                    new SqlParameter("@TranslatedString", string.IsNullOrEmpty(translateDictionary.TranslatedString) ? DBNull.Value : (object)translateDictionary.TranslatedString),
                    new SqlParameter("@TranslatedNormalizedString", string.IsNullOrEmpty(translateDictionary.TranslatedNormalizedString) ? DBNull.Value : (object)translateDictionary.TranslatedNormalizedString),
                    new SqlParameter("@DimensionSuffix", string.IsNullOrEmpty(translateDictionary.DimensionSuffix) ? DBNull.Value : (object)translateDictionary.DimensionSuffix)
                };
                command.Parameters.AddRange(parameters);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteTranslate(int idTranslate)
        {
            using (SqlCommand command = new SqlCommand("[Translate].[pub_deleteStringTranslate]", _connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StringID", idTranslate);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Dispose()
        {
            try
            {
                _connection.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
