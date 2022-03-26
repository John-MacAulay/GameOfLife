using GameOfLife;
using GameOfLife.UserInterfaces;

namespace GameOfLifeTests
{
    public class TestInput : IInput
    {
        private readonly string[] _fakeInput;
        private int _getInputPosition;
        private int _countUntilBreak;

        public TestInput(string[] fakeInput)
        {
            _fakeInput = fakeInput;
        }

        public string GetText()
        {
            return _fakeInput[_getInputPosition++];
        }

        public bool CheckForBreak()
        {
            _countUntilBreak++;
            return _countUntilBreak == 12;
        }
    }
}
