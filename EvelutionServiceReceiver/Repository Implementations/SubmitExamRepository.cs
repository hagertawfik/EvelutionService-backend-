using EvelutionServiceReceiver.DataContextEvelution;
using EvelutionServiceReceiver.Repository_Interfaces;

namespace EvelutionServiceReceiver.Repository_Implementations
{
    public class SubmitExamRepository:ISubmitExamRepository
    {
        private readonly DataContextEvelution1 _contextEvelution;
        public SubmitExamRepository(DataContextEvelution1 contextEvelution)
        {
            _contextEvelution = contextEvelution;
        }
      
    public int GetCorrectChoiceId(int questionId)
        {
            var correctChoiceId = _contextEvelution.Questions
                .Where(q => q.QuestionId == questionId)
                .Select(q => q.Choices.FirstOrDefault(c => c.IsCorrect).ChoiceId)
                .FirstOrDefault();
          if(correctChoiceId == null) { return 0; }
            return correctChoiceId; 
        }


    }
}
