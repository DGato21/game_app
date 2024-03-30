using Domain.Entities.TurtleChallenge;
using FluentValidation;

namespace Domain.Core.Validators
{
    public class TurtleGameValidator : AbstractValidator<GameState>
    {
        public TurtleGameValidator()
        {
        }
    }
}
