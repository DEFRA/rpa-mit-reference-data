using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Data;

namespace RPA.MIT.ReferenceData.Data
{
    public class CreateAndInsertSqlCommandInterceptor : DbCommandInterceptor
    {
        private readonly TextWriter _logStream;

        public CreateAndInsertSqlCommandInterceptor(TextWriter logStream)
        {
            _logStream = logStream;
        }

        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            if (command.CommandText.Contains("CREATE") || command.CommandText.Contains("DROP"))
            {
                LogCommand(command.CommandText);
            }
            return base.NonQueryExecuted(command, eventData, result);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            var r = base.ReaderExecuted(command, eventData, result);

            if (eventData.Command.CommandText.Contains("INSERT"))
            {
                var commands = eventData.Command.CommandText.Replace("\r\n", " ").Split(";");
                foreach (var c in commands.Where(c => c.Contains("INSERT")))
                {
                    var cr = ReplaceParams(c, command.Parameters);
                    LogCommand(cr);
                }
            }
            return r;
        }

        public string LogCommand(string commandText)
        {
            var logCommandText = $"{commandText.Trim().TrimEnd(';')};";
            _logStream.WriteLine(logCommandText);
            return logCommandText;
        }
        public static string ReplaceParams(string commandText, DbParameterCollection commandParams)
        {
            for (var i = commandParams.Count - 1; i >= 0; i--)
            {
                var param = commandParams[i];
                commandText = commandText.Replace(param.ParameterName, ParamString(param));
            }
            return commandText.Trim();
        }

        public static string ParamString(DbParameter commandParam)
        {
            switch (commandParam.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                    return $"'{commandParam.Value?.ToString()?.Replace("'","''")}'";

                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.Time:
                    return string.IsNullOrWhiteSpace(commandParam.Value?.ToString()) ? "null" : $"'{commandParam.Value}'";

                default:
                    return $"{commandParam.Value}";
            }
        }
    }
}
