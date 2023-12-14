
namespace HR.LeaveManagement.Application.Exceptions
{
// : Exception - paveldi Exception klase, kad NotFoundException galetu pats tapti kaip Exception ir taip veikti
    public class NotFoundException : Exception
    {
//CTOR turi du parametrus, string name, kuris perduos varda, o object - yra visu tipu saltinis.
// : base - reikalingas tam, kad butu galima perduoti zinute, kuri buti peduota kaip error zinute;
        public NotFoundException(string name, object key) : base($"{name} ({key} was not found)")
        {

        }
    }
}
