using FamsGames.Model;
using Npgsql;
using Npgsql.PostgresTypes;
using Dapper;


namespace FamsGames.Repositories
{
    public class FamsGamesRepository
    {
        private PosgreSQLConfig connexionString;
        public FamsGamesRepository(PosgreSQLConfig connectionString)
        {
            this.connexionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(connexionString.ConnectionString);
        }
        public async Task<IEnumerable<User>> SelectAllUsers()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                            FROM famsgames.users                          
                        ";

            return await db.QueryAsync<User>(sql, new { });
        }

        public async Task<IEnumerable<Score>> SelectAllScores()
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM famsgames.score";

            return await db.QueryAsync<Score>(sql, new { });
        }
        public async Task<IEnumerable<User>> SelectLocations()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT iduser, nickname, location
                            FROM famsgames.users                          
                        ";

            return await db.QueryAsync<User>(sql, new { });

        }
        public async Task<IEnumerable<Trivial>> SelectQuestions()
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                            FROM famsgames.preguntastrivia                          
                        ";

            return await db.QueryAsync<Trivial>(sql, new { });

        }
        public async Task<User> GetUser(string nickname, string password)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                            FROM famsgames.users
                            WHERE nickname = @Nickname AND password = @Password
                        ";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Nickname = nickname, Password = password });
        }

        public async Task<bool> CreateUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO famsgames.users (name, surname, nickname, password, location )
                        VALUES (@Name, @Surname, @Nickname, @Password, @Location)
                        ";

            var result = await db.ExecuteAsync(sql, new { user.Name, user.Surname, Nickname=user.Nickname, Password=user.Password, Location=user.Location });
            return result > 0;
        }

        public async Task<bool> CreateScore(Score score)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO famsgames.score ( idgame, iduser, points, tries )
                        VALUES (@IdGame, @IdUser, @Points, @Tries)
                        ";

            var result = await db.ExecuteAsync(sql, new { IdGame=score.IdGame, IdUser=score.IdUser, Points = score.Points, Tries = score.Tries});
            return result > 0;
        }
        ///
        public async Task<bool> UpdateScore(Score score)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE famsgames.score
                        SET
                            points = points + @Points,
                            tries = tries + 1
                        WHERE idgame = @IdGame AND iduser = @IdUser
                        ";

            var result = await db.ExecuteAsync(sql, new { IdUser = score.IdUser, IdGame = score.IdGame, Points = score.Points});
            return result > 0;
        }
        ///
        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE famsgames.users
                        SET name = @Name, 
                            surname = @Surname, 
                            nickname = @Nickname, 
                            password = @Password, 
                            location = @Location
                        WHERE iduser = @Iduser;
                        ";

            var result = await db.ExecuteAsync(sql, new { user.Name, user.Surname, user.Nickname,  user.Password, user.Location, user.IdUser});
            return result > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE FROM famsgames.users
                        WHERE iduser = @Id                           
                        ";

            var result = await db.ExecuteAsync(sql, new { Id=id });
            return result > 0;
        }
        public async Task<bool> DeleteScore(int id)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE FROM famsgames.score
                        WHERE iduser = @Id                           
                        ";

            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}
