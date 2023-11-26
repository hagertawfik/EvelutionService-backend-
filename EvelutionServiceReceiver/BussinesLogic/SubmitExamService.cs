using EvelutionServiceReceiver.BussinesLogicInterface;
using EvelutionServiceReceiver.DTO;
using EvelutionServiceReceiver.Repository_Interfaces;

namespace EvelutionServiceReceiver.BussinesLogic
{
    public class SubmitExamService: ISubmitExamService
    {
        private readonly ISubmitExamRepository _submitExamRepository;
       

        public SubmitExamService(ISubmitExamRepository submitExamRepository)
        {
          
            _submitExamRepository = submitExamRepository;
        }
        public int SubmitExam(SubmitExamRequestDto submitExamDto)
        {
           
            var totalCorrectAnswer = 0;

            foreach (var choice in submitExamDto.SelectedChoices)
            {
                var correctChoiceId = _submitExamRepository.GetCorrectChoiceId(choice.QuestionId);
                if (choice.ChoiceId == correctChoiceId)
                {
                    totalCorrectAnswer++;

                }
            }
           return totalCorrectAnswer;

        }
    }
}
