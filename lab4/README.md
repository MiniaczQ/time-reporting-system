# NTR21Z-Motyka-Jakub

Zadanie 4 na przedmiot NTR w semestrze zimowym 2021 - 2022.

Instrukcja uruchomienia:
 - pobrać repozytorium `git clone https://gitlab-stud.elka.pw.edu.pl/jmotyka/ntr21z-motyka-jakub`
 - wejść do katalogu projektu 4 `cd ntr21z-motyka-jakub\lab4`
 - *uruchomić bazę danych `docker-compose up`
 - *zbudować projekt `dotnet publish`
 - przejść do katalogu aplikacji `cd bin\Debug\net6.0\publish`
 - uruchomić aplikację `.\lab4.exe`
 - wejśc na stronę aplikacji `https://localhost:5001/`

Aplikacja udostępnia port 5001 dla HTTPS, i port 5000 dla HTTP.

* zadania mogą zająć do kilku minut, można je robić równolegle

Krótki opis danych testowych:
 - Aktywności są wpisane w dni 25-27 stycznia 2022
 - Użytkownik `James May` ma aktywny miesiąc
 - Użytkownik `Richard Hammons` ma zablokowany miesiąc
 - Użytkownik `Jeremy Clarkson` ma zablokowany miesiąc i zaakceptowaną aktywność w ramach projektu (projekt też jest zablokowany)
