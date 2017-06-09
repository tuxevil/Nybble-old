namespace ProjectBase.WebTester
{
[IgnoreFirst(1)]
[DelimitedRecord(";")]
public sealed class Prices
{

public System.String CodGrundfos;

public System.String CodProv;

public System.String Modelo;

public System.String Descripcion;

public System.String Proveedor;

[FieldOptional()]
public System.String Frecuencia;

[FieldOptional()]
[FieldConverter(typeof(MoneyConverter), ".")]
public Nullable < System.Decimal > TP;

[FieldOptional()]
public System.String MonedaTP;

[FieldOptional()]
[FieldConverter(ConverterKind.Decimal, ",")]
public Nullable < System.Decimal > GRP;

[FieldOptional()]
public System.String MonedaGRP;

[FieldOptional()]
[FieldConverter(ConverterKind.Decimal, ",")]
public Nullable < System.Decimal > PL;

[FieldOptional()]
public System.String MonedaPL;

[FieldOptional()]
public System.String Familia;

[FieldOptional()]
public System.String Tipo;

[FieldOptional()]
public System.String Linea;


}
}