using ShannonFano;


namespace FanoTests
{
    public class Tests
    {

        [Test]
        public void Code_PlainText_GetCode()
        {
            ShanFano shanFano = new ShanFano();

            string text = "Biba i boba";

            string expected = "1001110100101111101011100100";

            string code = shanFano.GetEncodedString(text);

            Assert.AreEqual(code, expected);
        }

        [Test]
        public void Decode_PlainText_GetSourceText()
        {
            ShanFano shanFano = new ShanFano();

            string text = "V bikini";

            string code = shanFano.GetEncodedString(text);

            string deCode = shanFano.GetDecodingString(code);

            Assert.AreEqual(text, deCode);
        }

        [Test]
        public void Code_EmptyText_Exception()
        {
            ShanFano shanFano = new ShanFano();

            string text = "";

            Assert.Throws<Exception>(() => shanFano.GetEncodedString(text));
        }
    }
}