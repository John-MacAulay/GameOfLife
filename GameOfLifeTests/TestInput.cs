using GameOfLife;

namespace GameOfLifeTests
{
    public class TestInput : IInput
    {
        private readonly string[] _fakeInput;
        private int _getInputPosition;

        public TestInput(string[] fakeInput)
        {
            _fakeInput = fakeInput;
        }

        public string GetText()
        {
            return _fakeInput[_getInputPosition++];
        }
    }
}
