#region Truncar cadenas
//var cadena = "1234567890qwertyuiop";
//Console.WriteLine(cadena);
//Console.WriteLine(cadena[..10]);
#endregion

#region Comparar un cacho de string
var cadena1 = "1234";
var cadena2 = "1234567890asdfghjklñ";

Console.WriteLine(cadena1 == cadena2[..4]);
#endregion