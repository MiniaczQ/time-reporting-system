# NTR21Z-Motyka-Jakub

Zadanie 1 na przedmiot NTR w semestrze zimowym 2021 - 2022.

Ze względu na zgodnośc z przykładowymi plikami .JSON:
- duża część kodu modeli i podmiotów służy wczytywaniu, zapisywaniu, a głównie łączeniu i filtrowaniu danych. Ponieważ następne zadanie polega na korzystaniu z bazy danych funkcje te są niekomentowane.
- część danych jest nadmiarowo enkapsulowana (np. oddzielna, jedno-polowa klasa dla `subcode`)
- nie ma back-endowej walidacji, bo wymaga to większej ilości ręcznego mapowania i filtrowania danych
- adresowanie podmiotów jest o wiele cięższe ze względu na brak unikalnych indeksów dla większości
- nazwy nie są w pełni oczywiste:
- `activity` - projekt który ma właściciela
- `entry` - zapis jednej osoby w ramach jednego `activity`
- `report` - zgrupowanie `entry` w jednym miesiącu

Poza tym, w projekcie dużo eksperymentowałem z różnymi konwencjami technologi C#, .NET CORE oraz Razor.
(np. różne sposoby wyświetlania informacji i tworzenia form w .CSHTML)