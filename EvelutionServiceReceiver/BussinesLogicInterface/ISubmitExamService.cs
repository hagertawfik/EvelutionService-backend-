using EvelutionServiceReceiver.DTO;

namespace EvelutionServiceReceiver.BussinesLogicInterface
{
    public interface ISubmitExamService
    {
        int SubmitExam(SubmitExamRequestDto submitExamDto);
    }
}
