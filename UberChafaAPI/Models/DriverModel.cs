using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UberChafaAPI.Models
{
    public class DriverModel
    {
        //string ConnectionString = "Data Source=" + "db-pinogordo.cyhnjqcokye6.us-east-2.rds.amazonaws.com" + ";Initial Catalog=" + "uberChafaDB" + ";User ID=" + "root" + ";Password=" + "nintendo123" + ";";
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public string Picture { get; set; }
        public PositionModel CurrentLocation { get; set; }
        public string Password { get; set; }

        public ObservableCollection<DriverModel> GetAll()
        {

            ObservableCollection<DriverModel> drivers = new ObservableCollection<DriverModel>();
            try
            {
                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "uberchafa.mysql.database.azure.com",
                    Database = "uberchafadb",
                    UserID = "uberadmin@uberchafa",
                    Password = "Nintendo123",
                    SslMode = MySqlSslMode.Required,
                };
                using (MySqlConnection sqlConnection = new MySqlConnection(builder.ConnectionString))
                {
                    sqlConnection.Open();
                    string queryString = "SELECT * FROM Driver;";
                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                drivers.Add(new DriverModel
                                {
                                    Id = (int)reader["Id"],
                                    Name = reader["Name"].ToString(),
                                    LicensePlate = reader["LicensePlate"].ToString(),
                                    Picture = reader["Picture"].ToString(),
                                    CurrentLocation = new PositionModel
                                    {
                                        Latitude = reader["Latitude"].ToString(),
                                        Longitude = reader["Longitude"].ToString()
                                    },
                                    Password = reader["Password"].ToString()
                                });
                            }
                        }
                    }
                }
                return drivers;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public DriverModel Get(int id)
        {
            DriverModel driver = new DriverModel();
            try//
            {
                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "uberchafa.mysql.database.azure.com",
                    Database = "uberchafadb",
                    UserID = "uberadmin@uberchafa",
                    Password = "Nintendo123",
                    SslMode = MySqlSslMode.Required,
                };
                using (MySqlConnection sqlConnection = new MySqlConnection(builder.ConnectionString))
                {
                    sqlConnection.Open();
                    string queryString = "SELECT * FROM Driver WHERE Id = " + this.Id +";";

                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                driver = new DriverModel
                                {
                                    Id = (int)reader["Id"],
                                    Name = reader["Name"].ToString(),
                                    LicensePlate = reader["LicensePlate"].ToString(),
                                    Picture = reader["Picture"].ToString(),
                                    CurrentLocation = new PositionModel
                                    {
                                        Latitude = reader["Latitude"].ToString(),
                                        Longitude = reader["Longitude"].ToString()
                                    },
                                    Password = reader["Password"].ToString()
                                };
                            }
                        }
                    }
                }
                return driver;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DriverModel GetLogin()
        {
            DriverModel driver = new DriverModel();
            try
            {
                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "uberchafa.mysql.database.azure.com",
                    Database = "uberchafadb",
                    UserID = "uberadmin@uberchafa",
                    Password = "Nintendo123",
                    SslMode = MySqlSslMode.Required,
                };
                using (MySqlConnection sqlConnection = new MySqlConnection(builder.ConnectionString))
                {
                    sqlConnection.Open();
                    string queryString = "SELECT * FROM Driver WHERE Name = '" + Name + "' and Password = '" + Password + "';";

                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                driver = new DriverModel
                                {
                                    Id = (int)reader["Id"],
                                    Name = reader["Name"].ToString(),
                                    LicensePlate = reader["LicensePlate"].ToString(),
                                    Picture = reader["Picture"].ToString(),
                                    CurrentLocation = new PositionModel
                                    {
                                        Latitude = reader["Latitude"].ToString(),
                                        Longitude = reader["Longitude"].ToString()
                                    },
                                    Password = reader["Password"].ToString()
                                };
                            }
                        }
                    }
                }
                return driver;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApiResponse Insert()
        {
            try
            {
                object Id;
                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "uberchafa.mysql.database.azure.com",
                    Database = "uberchafadb",
                    UserID = "uberadmin@uberchafa",
                    Password = "Nintendo123",
                    SslMode = MySqlSslMode.Required,
                };
                using (MySqlConnection sqlConnection = new MySqlConnection(builder.ConnectionString))
                {
                    sqlConnection.Open();
                    string queryString = "INSERT INTO `uberChafaDB`.`Driver` (`Name`, `LicensePlate`, `Picture`, `Latitude`, `Longitude`, `Password`) VALUES ('" 
                        + Name + "', '" 
                        + LicensePlate + "', '" 
                        + Picture + "', '" 
                        + CurrentLocation.Latitude + "', '"
                        + CurrentLocation.Longitude + "', '"
                        + Password +"'); SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        Id = cmd.ExecuteScalar();
                    }
                }
                if (Id != null && Id.ToString().Length > 0)
                {
                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Result = int.Parse(Id.ToString()),
                        Message = "¡Se ha creado correctamente!"
                    };

                }
                else
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Result = 0,
                        Message = "¡Error fatal! No se pudo crear el conductor"
                    };
                }
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = e.Message
                };
            }
        }

        public ApiResponse Update()
        {
            try
            {
                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "uberchafa.mysql.database.azure.com",
                    Database = "uberchafadb",
                    UserID = "uberadmin@uberchafa",
                    Password = "Nintendo123",
                    SslMode = MySqlSslMode.Required,
                };
                using (MySqlConnection sqlConnection = new MySqlConnection(builder.ConnectionString))
                {
                    sqlConnection.Open();
                    
                    string queryString = "";

                    queryString = "UPDATE `uberChafaDB`.`Driver` SET`Name` = '"
                            + Name + "', `LicensePlate` = '"
                            + LicensePlate + "', `Picture` = '"
                            + Picture + "', `Latitude` = '"
                            + CurrentLocation.Latitude + "', `Longitude` = '"
                            + CurrentLocation.Longitude + "', `Password` = '"
                            + Password + "' WHERE `Id` = "
                            + Id + ";";

                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteScalar();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = Id,
                    Message = "¡La información ha sido actualizada correctamente!"
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = e.Message
                };
            }
        }
        
        public ApiResponse Delete(int id)
        {
            try
            {
                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "uberchafa.mysql.database.azure.com",
                    Database = "uberchafadb",
                    UserID = "uberadmin@uberchafa",
                    Password = "Nintendo123",
                    SslMode = MySqlSslMode.Required,
                };
                using (MySqlConnection sqlConnection = new MySqlConnection(builder.ConnectionString))
                {
                    sqlConnection.Open();
                    string queryString = "DELETE FROM Driver WHERE Id = " + Id + ";";
                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Result = id,
                    Message = "¡Se ha eliminado correctamente!"
                };
            }
            catch (Exception e)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Result = null,
                    Message = e.Message
                };
            }
        }
    }
}
