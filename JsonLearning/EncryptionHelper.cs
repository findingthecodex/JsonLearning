namespace JsonLearning;

// det kommer vara en demo-klass som visar symmestrisk kryptering
// Klartext -> krypterar -> lagra -> läsa -> dekryptera - klartext

public class EncryptionHelper
{
    
    // En väldigt 
    // decimal 0-9
    
    // 0x42 är hexadecimalt (bas16). Det motsvarar 66 bytes i decimal (bas 10)
    // Värdet är taget från påhittat
    private const byte key = 0x42; // 66 bytes

    public static string Encrypt(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }
        
        // 1 kovertera texten till bytes
        // varför? Texten är Unicode (char/strings)
        // XOR för att kunna förvränga vår sträng och då behöver vi omvandla texten till en byte array
        
        var bytes = System.Text.Encoding.UTF8.GetBytes(text);
        
        /* 2.
         * En logisk operation
         * 0 ^ 0 = 0
         * 0 ^ 1 = 1
         * 1 ^ 0 = 0
         * 1 ^ 1 = 0
         * olika = 1, lika = 0
         *
         * Varför just XOR?
         *  - Enkelt att förstå
         *  - Symmetriskt: (A ^ K) ^ K = A
         *
         * Varför (byte)(bytes[i] ^ key)
         *  - bytes[i] är en byte (0-255)
         *  - Key är också en byte
         *  - bytes[i] ^ Key ger ett int-resultat, så vi castar tillbaka det till byte
         */
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte)(bytes[i] ^ key);
        }
        
        // 3 för att kunna spara resultatet som text. Kodar vi bytes till Base64.
        // Varför Base64?
        // Efter vi har gjort XOR kan bytes innehålla obegripliga eller ogiltiga tecken för text/JSON.
        // Lättare att lagra filer JSON, Databaser osv.
        return Convert.ToBase64String(bytes);
    }

    public static string Decrypt(string krypteradText)
    {
        // 1
        if (string.IsNullOrEmpty(krypteradText))
        {
            return krypteradText;
        }

        // 2
        // Gör om Base64-strängen till bytes igen
        // XOR tillbaka med samma nyckel
        // Här utnyttjar vi XOR-egenskapen
        // Orginaltext ^ key ? krypterad
        // krypteradtext ^ Key = original
        // Därför ser koden exakt likadan ut
        var bytes = Convert.FromBase64String(krypteradText);

        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i]=(byte)(bytes[i] ^ key);
        }
        
        // 3 Konverterar tillbaka från bytes -> Klartext med UTF8
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

}