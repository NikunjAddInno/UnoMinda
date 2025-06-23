using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tryThresholds.IP_tools;

namespace VoltasBeko
{
    public class LogReportData
    {
        public LogReportData(LogReportData logReportData)
        {
            Date = logReportData.Date;
            Time = logReportData.Time;
            ModelName = logReportData.ModelName;
            ModelCode = logReportData.ModelCode;
            Defects = logReportData.Defects;
            Result = logReportData.Result;
            DefectImage = logReportData.DefectImage;
            PoseNumber = logReportData.PoseNumber;
            InspectionCount = logReportData.InspectionCount;

        }
        public LogReportData()
        {

        }

        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string ModelName { get; set; }
        public string ModelCode { get; set; }
        public string Defects { get; set; }
        public bool Result { get; set; }
        public Bitmap DefectImage { get; set; }
        public int PoseNumber { get; set; }
        public int InspectionCount { get; set; }
    }

    public static class Database
    {
        static string tableName = "logreport";


        static Database()
        {
            //CreateLogReportTableIfNotExists();
            //CreateFinalLogReportTableIfNotExists();
        }

        public static void CreateLogReportTableIfNotExists()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Create the table if it does not exist
                    cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS logreport (
                        _date DATE,
                        _time TIME WITHOUT TIME ZONE,
                        modelname VARCHAR,
                        modelcode VARCHAR,
                        defects VARCHAR,
                        result BOOLEAN,
                        defectimage BYTEA,
                        posenumber int
                    )";
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("logreport table created successfully or already exists.");
        }
        public static void CreateFinalLogReportTableIfNotExists()
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Create the table if it does not exist
                    cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS final_logreport (
                _date DATE,
                _time TIME WITHOUT TIME ZONE,
                modelname VARCHAR,
                modelcode VARCHAR,
                result BOOLEAN,
                inspection_count INT
            )";
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("final_logreport table created successfully or already exists.");
        }

        public static List<LogReportData> GetReworkDataByModelCode(string modelCode)
        {
            List<LogReportData> dataList = new List<LogReportData>();

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    //cmd.CommandText = $@"
                    //SELECT _date, _time, modelname, modelcode, defects, result, defectimage, posenumber
                    //FROM logreport
                    //WHERE modelcode = @modelCode and _date = '{DateTime.Now.ToString("yyyy-MM-dd")}' and result = false order by _date desc, _time desc LIMIT {count}";

                    cmd.CommandText = $@"
                                SELECT _date, _time, modelname, modelcode, defects, result, defectimage, posenumber, inspection_count
                                FROM logreport
                                WHERE modelcode = @modelCode 
                                AND _date = '{DateTime.Now.ToString("yyyy-MM-dd")}' 
                                AND result = false 
                                AND inspection_count = (
                                    SELECT MAX(inspection_count) 
                                    FROM logreport 
                                    WHERE modelcode = @modelCode
                                )
                                ORDER BY _date DESC, _time DESC";
                                

                    cmd.Parameters.AddWithValue("@modelCode", modelCode);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new LogReportData
                            {
                                Date = reader.GetDateTime(0),
                                Time = Convert.ToDateTime(reader.GetFieldValue<TimeSpan>(1).ToString()),
                                ModelName = reader.GetString(2),
                                ModelCode = reader.GetString(3),
                                Defects = reader.GetString(4),
                                Result = reader.GetBoolean(5),
                                DefectImage = ByteArrayToBitmap(reader.GetFieldValue<byte[]>(6)),
                                PoseNumber = reader.GetInt32(7),
                                InspectionCount = reader.GetInt32(8)
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }

            return dataList;
        }

        public static List<LogReportData> GetReworkDataBackCamByModelCode(string modelCode)
        {
            List<LogReportData> dataList = new List<LogReportData>();

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    //cmd.CommandText = $@"
                    //SELECT _date, _time, modelname, modelcode, defects, result, defectimage, posenumber
                    //FROM logreport
                    //WHERE modelcode = @modelCode and _date = '{DateTime.Now.ToString("yyyy-MM-dd")}' and result = false order by _date desc, _time desc LIMIT {count}";

                    cmd.CommandText = $@"
                                SELECT _date, _time, modelname, modelcode, defects, result, defectimage, camnumber, inspection_count
                                FROM logreport_backcamera
                                WHERE modelcode = @modelCode 
                                AND result = false 
                                AND inspection_count = (
                                    SELECT MAX(inspection_count) 
                                    FROM logreport_backcamera 
                                    WHERE modelcode = @modelCode
                                )
                                ORDER BY _date DESC, _time DESC";


                    cmd.Parameters.AddWithValue("@modelCode", modelCode);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new LogReportData
                            {
                                Date = reader.GetDateTime(0),
                                Time = Convert.ToDateTime(reader.GetFieldValue<TimeSpan>(1).ToString()),
                                ModelName = reader.GetString(2),
                                ModelCode = reader.GetString(3),
                                Defects = reader.GetString(4),
                                Result = reader.GetBoolean(5),
                                DefectImage = ByteArrayToBitmap(reader.GetFieldValue<byte[]>(6)),
                                PoseNumber = reader.GetInt32(7),
                                InspectionCount = reader.GetInt32(8)
                            };
                            dataList.Add(data);
                        }
                    }
                }
            }

            Console.WriteLine($"dataList: {dataList.Count}");

            return dataList;
        }


        public static List<LogReportData> GetPartDataByModelCode(string modelCode)
        {
            List<LogReportData> dataList = new List<LogReportData>();

            using (NpgsqlConnection conn = GetConnection())
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = $@"
                    SELECT _date, _time, modelname, modelcode, defects, result, defectimage, posenumber
                    FROM logreport
                    WHERE modelcode = @modelCode AND result = false";

                    cmd.Parameters.AddWithValue("@modelCode", modelCode);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = new LogReportData
                            {
                                Date = reader.GetDateTime(0),
                                Time = Convert.ToDateTime(reader.GetFieldValue<TimeSpan>(1).ToString()),
                                ModelName = reader.GetString(2),
                                ModelCode = reader.GetString(3),
                                Defects = reader.GetString(4),
                                Result = reader.GetBoolean(5),
                                DefectImage = ByteArrayToBitmap(reader.GetFieldValue<byte[]>(6)),
                                PoseNumber = reader.GetInt32(7)
                            };
                            dataList.Add(data);
                        }
                    }
                }
                conn.Close();
            }

            return dataList;
        }


        public static List<LogReportData> GetBackCamReportByModelCode(string modelCode, int inspectionCount)
        {
            List<LogReportData> logDataList = new List<LogReportData>();

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $@"SELECT _date, _time, modelname, modelcode, defects, defectimage, camnumber, inspection_count, result
                                    FROM public.logreport_backcamera
                                    WHERE inspection_count = {inspectionCount} and modelcode = @ModelCode";

                    cmd.Parameters.AddWithValue("@ModelCode", modelCode);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LogReportData logData = new LogReportData
                            {
                                Date = reader.GetDateTime(0),
                                Time = Convert.ToDateTime(reader.GetFieldValue<TimeSpan>(1).ToString()),
                                ModelName = reader.GetString(2),
                                ModelCode = reader.GetString(3),
                                Defects = reader.GetString(4),
                                DefectImage = reader["defectimage"] != DBNull.Value ? ByteArrayToImage((byte[])reader["defectimage"]) : null,
                                PoseNumber = reader.GetInt32(6),
                                InspectionCount = reader.GetInt32(7),
                                Result = reader.GetBoolean(8)
                            };
                            logDataList.Add(logData);
                        }
                    }
                }
            }

            return logDataList;
        }

        // Utility function to convert byte array to Bitmap
        private static Bitmap ByteArrayToImage(byte[] byteArray)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray))
            {
                return new Bitmap(ms);
            }
        }


        public static void CheckAndUpdateReworkCount(string modelname)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();

                    // Check if data in logreport matches for the specified modelname and posenumber
                    using (var cmdCheck = new NpgsqlCommand())
                    {
                        cmdCheck.Connection = conn;
                        cmdCheck.CommandText = @"
                        SELECT COUNT(*)
                        FROM logreport
                        WHERE modelname = @modelname AND result = @result";

                        cmdCheck.Parameters.AddWithValue("@modelname", modelname);
                        cmdCheck.Parameters.AddWithValue("@result", false);

                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (count > 0)
                        {
                            // Update rework count in reworkstation table
                            using (var cmdUpdate = new NpgsqlCommand())
                            {
                                cmdUpdate.Connection = conn;
                                cmdUpdate.CommandText = @"
                                UPDATE reworkstation
                                SET reworkcount = reworkcount + 1
                                WHERE modelname = @modelname";

                                cmdUpdate.Parameters.AddWithValue("@modelname", modelname);

                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // No matching data found in logreport table
                            Console.WriteLine("No matching data found in logreport table.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static Bitmap ByteArrayToBitmap(byte[] byteArray)
        {
            if (byteArray == null || byteArray == new byte[0] || byteArray.Length <= 0)
            {
                Bitmap bitmap = new Bitmap(200, 200);
                return bitmap;
            }
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                return new Bitmap(stream);
            }
        }

        public static void InsertDataIntoLogReport(LogReportData data)
        {
            //await Task.Delay(1);
            byte[] imageData = new byte[0];

            if (data.DefectImage != null)
            {
                // Convert Bitmap to byte array
                using (var stream = new MemoryStream())
                {
                    data.DefectImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    imageData = stream.ToArray();
                }
            }
           

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;


                    // Delete existing records where modelname = modelname and result = true
                    cmd.CommandText = "DELETE FROM logreport WHERE modelcode = @modelCode AND posenumber = @posenumber";
                    cmd.Parameters.AddWithValue("@modelCode", data.ModelCode);
                    cmd.Parameters.AddWithValue("@posenumber", data.PoseNumber);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                    INSERT INTO logreport (_date, _time, modelname, modelcode, defects, result, defectimage, posenumber)
                    VALUES (@date, @time, @modelName, @modelCode, @defects, @result, @defectImage, @posenumber)";

                    cmd.Parameters.AddWithValue("@date", data.Date);
                    cmd.Parameters.AddWithValue("@time", data.Time);
                    cmd.Parameters.AddWithValue("@modelName", data.ModelName);
                    cmd.Parameters.AddWithValue("@modelCode", data.ModelCode);
                    cmd.Parameters.AddWithValue("@defects", data.Defects);
                    cmd.Parameters.AddWithValue("@result", data.Result);
                    cmd.Parameters.AddWithValue("@defectImage", imageData);
                    cmd.Parameters.AddWithValue("@posenumber", data.PoseNumber);

                    cmd.ExecuteNonQuery();
                }
            }

            AppendDataToCsv(data);
            Console.WriteLine("Data inserted successfully into logreport table.");
        }

        public static void AppendDataToCsv(LogReportData data)
        {
            string monthYear = DateTime.Now.ToString("MM_yyyy");
            string fileName = $"{data.ModelName}_{monthYear}.csv";
            string filePath = Path.Combine(AppData.CsvPath, fileName);
            ConsoleExtension.WriteWithColor(filePath, ConsoleColor.Yellow);


            tryThresholds.IP_tools.resultFrontCam result = JsonConvert.DeserializeObject<tryThresholds.IP_tools.resultFrontCam>(data.Defects);
            data.Defects = JsonConvert.SerializeObject(result.list_defectDetails.Select(r => r.DefectName));

            // Prepare the CSV line
            string csvLine = $"{data.Date},{data.Time},{data.ModelName},{data.ModelCode},{data.Defects},{data.Result},{data.PoseNumber}";

            // Check if the file exists to add headers if necessary
            bool fileExists = File.Exists(filePath);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Date,Time,ModelName,ModelCode,Defects,Result,PoseNumber");
                }
                writer.WriteLine(csvLine);
            }
        }

        public static bool GetResultByModelCode(string modelCode)
        {
            bool result = false;

            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"SELECT result
                                    FROM public.final_logreport
                                    WHERE modelcode = @ModelCode
                                    ORDER BY inspection_count DESC
                                    LIMIT 1";

                    cmd.Parameters.AddWithValue("@ModelCode", modelCode);

                    var queryResult = cmd.ExecuteScalar();
                    if (queryResult != null && queryResult != DBNull.Value)
                    {
                        result = Convert.ToBoolean(queryResult);
                    }
                }
            }

            return result;
        }


        public static void InsertReworkData(DateTime date, DateTime time, string modelName, string modelCode)
        {
            using (var conn = GetConnection())
            {
                conn.Open();



                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $@"INSERT INTO public.reworkstation (_date, _time, modelname, modelcode, rework_count)
                                    VALUES (@date, @time, @modelName, @modelCode, (SELECT COUNT(*) + 1 FROM reworkstation WHERE modelcode = '{modelCode}'))";
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@modelName", modelName);
                    cmd.Parameters.AddWithValue("@modelCode", modelCode);
                    cmd.ExecuteNonQuery();
                }
            }
        }




        public static void  InsertDataIntoLogReport(LogReportData data, int poseNumber)
        {
            //await Task.Delay(1);
            byte[] imageData;

            // Convert Bitmap to byte array
            using (var stream = new MemoryStream())
            {
                data.DefectImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                imageData = stream.ToArray();
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                // Begin transaction
                using (var transaction = conn.BeginTransaction())
                {
                    //// Delete existing records where modelcode and posenumber matches
                    //using (var deleteCmd = new NpgsqlCommand())
                    //{
                    //    deleteCmd.Connection = conn;
                    //    deleteCmd.Transaction = transaction;
                    //    deleteCmd.CommandText = @"
                    //DELETE FROM logreport
                    //WHERE modelcode = @modelCode AND posenumber = @posenumber";

                    //    deleteCmd.Parameters.AddWithValue("@modelCode", data.ModelCode);
                    //    deleteCmd.Parameters.AddWithValue("@posenumber", data.PoseNumber);

                    //    deleteCmd.ExecuteNonQuery();
                    //}

                    // Insert new record
                    using (var insertCmd = new NpgsqlCommand())
                    {
                        insertCmd.Connection = conn;
                        insertCmd.Transaction = transaction;
                        insertCmd.CommandText = @"
                    INSERT INTO logreport (_date, _time, modelname, modelcode, defects, result, defectimage, posenumber, inspection_count)
                    VALUES (@date, @time, @modelName, @modelCode, @defects, @result, @defectImage, @posenumber, @inspectionCount)";

                        insertCmd.Parameters.AddWithValue("@date", data.Date);
                        insertCmd.Parameters.AddWithValue("@time", data.Time);
                        insertCmd.Parameters.AddWithValue("@modelName", data.ModelName);
                        insertCmd.Parameters.AddWithValue("@modelCode", data.ModelCode);
                        insertCmd.Parameters.AddWithValue("@defects", data.Defects);
                        insertCmd.Parameters.AddWithValue("@result", data.Result);
                        insertCmd.Parameters.AddWithValue("@defectImage", imageData);
                        insertCmd.Parameters.AddWithValue("@posenumber", poseNumber);
                        insertCmd.Parameters.AddWithValue("@inspectionCount", data.InspectionCount);
                        ConsoleExtension.WriteWithColor($"Pose number: {data.PoseNumber} logReportData.ModelCode: {data.ModelCode}", ConsoleColor.Yellow);

                        insertCmd.ExecuteNonQuery();
                    }

                    // Commit transaction
                    transaction.Commit();
                }
            }

            Console.WriteLine("Data inserted successfully into logreport table.");
        }

        //public static void InsertDataIntoLogReport(DateTime date, DateTime time, string modelName, string modelCode, string defects, bool result, Bitmap defectImage)
        //{
        //    byte[] imageData;

        //    // Convert Bitmap to byte array
        //    using (var stream = new MemoryStream())
        //    {
        //        defectImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        imageData = stream.ToArray();
        //    }

        //    using (var conn = GetConnection())
        //    {
        //        conn.Open();

        //        using (var cmd = new NpgsqlCommand())
        //        {
        //            cmd.Connection = conn;

        //            cmd.CommandText = @"
        //            INSERT INTO logreport (_date, _time, modelname, modelcode, defects, result, defectimage)
        //            VALUES (@date, @time, @modelName, @modelCode, @defects, @result, @defectImage)";

        //            cmd.Parameters.AddWithValue("@date", date);
        //            cmd.Parameters.AddWithValue("@time", time);
        //            cmd.Parameters.AddWithValue("@modelName", modelName);
        //            cmd.Parameters.AddWithValue("@modelCode", modelCode);
        //            cmd.Parameters.AddWithValue("@defects", defects);
        //            cmd.Parameters.AddWithValue("@result", result);
        //            cmd.Parameters.AddWithValue("@defectImage", imageData);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }

        //    Console.WriteLine("Data inserted successfully into logreport table.");
        //}


        public static int GetEntryCount(string modelCode)
        {
            using (NpgsqlConnection connection = GetConnection())
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM public.final_logreport WHERE modelcode = @ModelCode";
                    command.Parameters.AddWithValue("ModelCode", modelCode);

                    object result = command.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        return 1; // If no records found, return 1
                    }
                    else
                    {
                        int count = Convert.ToInt32(result) + 1;
                        return count;
                    }
                }
            }
        }

        public static void InsertDataFinalReport(LogReportData logData)
        {
            using (NpgsqlConnection connection = GetConnection())
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "DELETE FROM final_logreport WHERE modelcode = @modelCode";
                    command.Parameters.AddWithValue("@modelCode", logData.ModelCode);
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO public.final_logreport (_date, _time, modelname, modelcode, result, inspection_count)
                                        VALUES (@Date, @Time, @ModelName, @ModelCode, @Result, @InspectionCount)";
                    command.Parameters.AddWithValue("Date", logData.Date);
                    command.Parameters.AddWithValue("Time", logData.Time);
                    command.Parameters.AddWithValue("ModelName", logData.ModelName);
                    command.Parameters.AddWithValue("ModelCode", logData.ModelCode);
                    command.Parameters.AddWithValue("Result", logData.Result);
                    command.Parameters.AddWithValue("InspectionCount", logData.InspectionCount);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SetResultTrueByModelCode(string modelCode)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    // Update result to true in logreport
                    cmd.CommandText = "UPDATE logreport SET result = true WHERE modelcode = @modelCode";
                    cmd.Parameters.AddWithValue("@modelCode", modelCode);
                    cmd.ExecuteNonQuery();

                    // Update result to true in final_logreport
                    cmd.CommandText = "UPDATE final_logreport SET result = true WHERE modelcode = @modelCode";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@modelCode", modelCode);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Result set to true for modelCode: " + modelCode + " in both logreport and final_logreport tables.");
        }
        public static void InsertDataBackCam(LogReportData logData)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO public.logreport_backcamera (_date, _time, modelname, modelcode, defects, defectimage, camnumber, inspection_count, result)
                                    VALUES (@Date, @Time, @ModelName, @ModelCode, @Defects, @DefectImage, @CamNumber, @InspectionCount, @Result)";

                    cmd.Parameters.AddWithValue("@Date", logData.Date);
                    cmd.Parameters.AddWithValue("@Time", logData.Time.TimeOfDay);
                    cmd.Parameters.AddWithValue("@ModelName", logData.ModelName);
                    cmd.Parameters.AddWithValue("@ModelCode", logData.ModelCode);
                    cmd.Parameters.AddWithValue("@Defects", logData.Defects);
                    cmd.Parameters.AddWithValue("@DefectImage", ImageToByteArray(logData.DefectImage));
                    cmd.Parameters.AddWithValue("@CamNumber", logData.PoseNumber); // PoseNumber is CamNumber
                    cmd.Parameters.AddWithValue("@InspectionCount", logData.InspectionCount);
                    cmd.Parameters.AddWithValue("@Result", logData.Result);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteOldLogReportBackCameraData()
        {
            string query = "DELETE FROM public.logreport_backcamera WHERE _date < CURRENT_DATE - INTERVAL '5 days';";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Deleted {rowsAffected} rows from logreport_backcamera.");
                }
            }
        }

        public static void DeleteOldLogReportData()
        {
            string query = "DELETE FROM public.logreport WHERE _date < CURRENT_DATE - INTERVAL '5 days';";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Deleted {rowsAffected} rows from logreport.");
                }
            }
        }


        // Utility function to convert Bitmap to byte array
        private static byte[] ImageToByteArray(Bitmap image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));
        }
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server = localhost; Port = 5432; user Id = postgres; password = 1234; Database = postgres; Timeout = 300; CommandTimeout = 30;");
        }
    }


}