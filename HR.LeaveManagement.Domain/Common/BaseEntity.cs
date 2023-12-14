
namespace HR.LeaveManagement.Domain.Common;

public abstract class BaseEntity // abstract klase, kad negaletu veikti savarankiskai
    //cia laikomi value, kurie kartojasi per visas klases, todel folder ir pavadintas Common
{
    public int Id { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}
