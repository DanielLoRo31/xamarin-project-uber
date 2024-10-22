﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace UberChafaAPI.Models
{
    public class TripModel
    {
        //string ConnectionString = "Data Source=" + "db-pinogordo.cyhnjqcokye6.us-east-2.rds.amazonaws.com" + ";Initial Catalog=" + "uberChafaDB" + ";User ID=" + "root" + ";Password=" + "nintendo123" + ";";
        public int Id { get; set; }
        public int IdDriver { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public string OriginAddress { get; set; }
        public string OriginCoordinates { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCoordinates { get; set; }
        public int Status { get; set; }
        public string Route { get; set; }

        public ObservableCollection<TripModel> GetAll()
        {

            ObservableCollection<TripModel> trips = new ObservableCollection<TripModel>();
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
                    string queryString = "SELECT * FROM Trip;";
                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trips.Add(new TripModel
                                {
                                    Id = (int)reader["Id"],
                                    IdDriver = (int)reader["IdDriver"],
                                    InitialDate = (DateTime)reader["InitialDate"],
                                    FinalDate = (DateTime)reader["FinalDate"],
                                    OriginAddress = reader["OriginAddress"].ToString(),
                                    OriginCoordinates = reader["OriginCoordinates"].ToString(),
                                    DestinationAddress = reader["DestinationAddress"].ToString(),
                                    DestinationCoordinates = reader["DestinationCoordinates"].ToString(),
                                    Status = (int)reader["Status"],
                                    Route = reader["Route"].ToString()
                                });
                            }
                        }
                    }
                }
                return trips;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TripModel Get(int id)
        {
            TripModel trip = new TripModel();
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
                    string queryString = "SELECT * FROM Trip WHERE Id = " + id + ";";

                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trip = new TripModel
                                {
                                    Id = (int)reader["Id"],
                                    IdDriver = (int)reader["IdDriver"],
                                    InitialDate = (DateTime)reader["InitialDate"],
                                    FinalDate = (DateTime)reader["FinalDate"],
                                    OriginAddress = reader["OriginAddress"].ToString(),
                                    OriginCoordinates = reader["OriginCoordinates"].ToString(),
                                    DestinationAddress = reader["DestinationAddress"].ToString(),
                                    DestinationCoordinates = reader["DestinationCoordinates"].ToString(),
                                    Status = (int)reader["Status"],
                                    Route = reader["Route"].ToString()
                                };
                            }
                        }
                    }
                }
                return trip;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TripModel GetActualTrip(int id)
        {
            TripModel trip = new TripModel();
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
                    string queryString = "SELECT * FROM Trip WHERE IdDriver = " + id + " and Status = 1;";

                    using (MySqlCommand cmd = new MySqlCommand(queryString, sqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trip = new TripModel
                                {
                                    Id = (int)reader["Id"],
                                    IdDriver = (int)reader["IdDriver"],
                                    InitialDate = (DateTime)reader["InitialDate"],
                                    FinalDate = (DateTime)reader["FinalDate"],
                                    OriginAddress = reader["OriginAddress"].ToString(),
                                    OriginCoordinates = reader["OriginCoordinates"].ToString(),
                                    DestinationAddress = reader["DestinationAddress"].ToString(),
                                    DestinationCoordinates = reader["DestinationCoordinates"].ToString(),
                                    Status = (int)reader["Status"],
                                    Route = reader["Route"].ToString()
                                };
                            }
                        }
                    }
                }
                return trip;
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
                    string queryString = "INSERT INTO `uberchafadb`.`trip` (`IdDriver`, `OriginAddress`, `OriginCoordinates`, `DestinationAddress`, `DestinationCoordinates`, `Status`, `Route`) VALUES ('"
                        + IdDriver + "', '"
                        + OriginAddress + "', '"
                        + OriginCoordinates + "', '"
                        + DestinationAddress + "', '"
                        + DestinationCoordinates + "', '"
                        + Status + "', '"
                        + Route + "'); SELECT LAST_INSERT_ID();";
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
                        Message = "¡Error fatal! No se pudo crear el viaje"
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
                    string queryString = "DELETE FROM Trip WHERE Id = " + Id + ";";
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

                    queryString = "UPDATE `uberchafadb`.`trip` SET `FinalDate` = "
                            + "current_timestamp()" + ", `Status` = "
                            + Status + ", `Route` = '"
                            + Route + "' WHERE `Id` = "
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
    }
}
