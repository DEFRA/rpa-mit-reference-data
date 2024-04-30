using RPA.MIT.ReferenceData.Data;
using Moq;
using Npgsql;

namespace RPA.MIT.ReferenceData.Api.Test.Interceptor
{
    public class CreateAndInsertSQLCommandInterceptorTest
    {
        private CreateAndInsertSqlCommandInterceptor _createAndInsertSQLCommandInterceptor;
        private Mock<TextWriter> _Logger;

        public CreateAndInsertSQLCommandInterceptorTest() {
            _Logger = new Mock<TextWriter>();
            _Logger.Setup(s => s.WriteLine(It.IsAny<string>()));
            _createAndInsertSQLCommandInterceptor = new CreateAndInsertSqlCommandInterceptor(_Logger.Object);
        }

        [Theory]
        [InlineData("CommandText1", "CommandText1;")]
        [InlineData("CommendText2;", "CommendText2;")]
        [InlineData("CommandText3;;", "CommandText3;")]
        public void LogCommandLogsCorrectly(string commandToLog, string expectedLogText)
        {
            var loggedText = _createAndInsertSQLCommandInterceptor.LogCommand(commandToLog);
             Assert.Equal(loggedText,expectedLogText);
            _Logger.Verify(x => x.WriteLine(expectedLogText), Times.Once());
        }

        [Fact]
        public void ReplaceParamsWorksCorrectly() {

            var testDate = DateTime.Now;
            var command = new NpgsqlCommand("commandText @p0,@p1,@p2,@p3,@p4");
            command.Parameters.Add("@p0", NpgsqlTypes.NpgsqlDbType.Text).Value = "p0value";
            command.Parameters.Add("@p1", NpgsqlTypes.NpgsqlDbType.Date).Value = testDate;
            command.Parameters.Add("@p2", NpgsqlTypes.NpgsqlDbType.Date).Value = null;
            command.Parameters.Add("@p3", NpgsqlTypes.NpgsqlDbType.Integer).Value = 33;
            command.Parameters.Add("@p4", NpgsqlTypes.NpgsqlDbType.Text).Value = "Apostro'phe";

            var replacedCommandText = CreateAndInsertSqlCommandInterceptor.ReplaceParams(command.CommandText, command.Parameters);
            Assert.Equal(replacedCommandText,$"commandText 'p0value','{testDate}',null,33,'Apostro''phe'");
        }
    }
}
