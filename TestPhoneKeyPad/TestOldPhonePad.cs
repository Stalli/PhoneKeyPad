using PhoneKeyPad;

namespace TestPhoneKeyPad;

public class OldPhonePadTests
{
    [TestCase("33#", "e")]
    [TestCase("227*#", "b")]
    [TestCase("4433555 555666#", "hello")]
    [TestCase("8 88777444666*664#", "turing")]
    [TestCase(" 33#", "e")]
    [TestCase("3333#", "d")]
    [TestCase("33333#", "e")]
    [TestCase("333333#", "f")]
    [TestCase("3333333#", "d")]
    [TestCase("34* #", "d")]
    public void BasicTest(string input, string expectedOutcome)
    {
        Assert.That(OldPhoneTyper.OldPhonePad(input), Is.EqualTo(expectedOutcome));
    }
    
    [TestCase("#")]
    [TestCase("11#")]
    [TestCase("33*#")]
    [TestCase("33**#")]
    [TestCase("*#")]
    [TestCase("3 *#")]
    [TestCase("3 3**#")]
    [TestCase("3 3 **#")]
    [TestCase("34***#")]
    public void ExpectEmpty(string input)
    {
        Assert.That(OldPhoneTyper.OldPhonePad(input), Is.Empty);
    }
    
    [TestCase("3344")]
    [TestCase("33#44")]
    [TestCase("")]
    [TestCase("w#")]
    public void ExpectException(string input)
    {
        Assert.That(() => OldPhoneTyper.OldPhonePad(input), Throws.ArgumentException);
    }
}