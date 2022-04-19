namespace mniaAPI.Services
{
    public class ValidateCPF
    {
        public static bool CPF(string cpf)
        {

            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int som;
            int rest;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            som = 0;

            for (int i = 0; i < 9; i++)
                som += int.Parse(tempCpf[i].ToString()) * mult1[i];

            rest = som % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();

            tempCpf = tempCpf + digit;

            som = 0;

            for (int i = 0; i < 10; i++)
                som += int.Parse(tempCpf[i].ToString()) * mult2[i];

            rest = som % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            return cpf.EndsWith(digit);
        }

    }
}