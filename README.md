# Dokumentacja Użytkowa Aplikacji Dziennik

## Wstęp

"Dziennik" to aplikacja webowa ASP.NET Core MVC, która umożliwia zarządzanie grupami studenckimi.

## Wymagania Systemowe

- System operacyjny: Windows 10 lub nowszy, Linux, macOS
- .NET Runtime: .NET 5.0 lub nowszy
- Przeglądarka internetowa: najnowsza wersja Chrome, Firefox, Safari lub Edge

## Instalacja

Aplikacja jest dostarczana jako plik .exe. Aby zainstalować aplikację, otwórz Eksplorator Windows, przejdź do lokalizacji pliku .exe i kliknij dwukrotnie na plik .exe, aby uruchomić instalator.

## Jak korzystać z aplikacji

### Rejestracja

W prawym górnym rogu aplikacji widzimy odnośnik do formularza rejestracji. Po podaniu swoich danych w formularzu zatwierdzamy rejestrację przyciskiem "Register". Następnie zatwierdzamy swój email linkiem na stronie.

### Logowanie

Również w prawym górnym rogu znajduje się odnośnik formularza logowania. Użyj go do zalogowania się.

### Prośba o przydzielenie roli

Po zalogowaniu naciśnij na swoją nazwę użytkownika, po czym wybierz podzakładkę "Role Requests". Wybierz insteresującą Cię rolę i zatwierdź formularz.

UWAGA: Zatwierdzenia roli może dokonać tylko administrator, dlatego rolę pierwszego administratora nalerzy zatwierdzić ręcznie poprzez modyfikację odpowiedniego rekordu w tabeli RoleRequests oraz ASPUserRoles bazy danych, aby dokonać powiązania administratora z rolą.

Kolejnych twierdzeń dokonuje administrator z zakładki UserManagement -> Role Requests.

### Operacje początkowe

Aby aplikacja mogła z powodzeniem śledzić działanie szkoły pewne dane muszą być początkowo wprowadzone do systemu. Najlepszą rolą do dokonywania tych zmian jest administrator. Do takich zadań należą:
- Akceptacja nauczycieli, uczniów oraz rodziców,
- Stworzenie klas (grup dydaktycznich) (zakładka Groups)
- Przypisanie uczniów do klas
- Zdefiniowanie programów przedmiotów dla poszczególnych przedmiotów na konkretnych stopniach nauczania (zakładka Course Programs) programy nauczania stanowią szablony do definiowania kursów, które dotyczą już konkretnej grupy w konkretnym roku szkolnym.
- Definiowanie kursów (przedmiotów)
- Przypisywanie nauczycieli jako wychowawców klas (w zakładce Groups) oraz nauczycieli przedmiotów (w zakładce Courses)

### Operacje specyficzne dla nauczycieli

Strona główna nauczyciela wyświetla zawsze aktualną lub najbliższą lekcję i formularz obecności dla tej lekcji. Z tego poziomu nauczyciel może ustawić obecność dla wszystkich uczniów. Poniżej znajduje się chronologiczna lista kolejnych nadchodzących lekcji.

W widoku kursów nauczyciele mogą wprowadzać nowe zadania (Tasks) a dla każdego z nich przypisywać uczniom oceny (Marks).

### Operacje specyficzne dla ucznia

Strona główna ucznia zawiera podobnie listę lekcji obowiązujących ucznia w kolejności chronologicznej. Dodatkowo uczeń ma dostęp do widoków podsumowania ocen (średnia ważona) dla wszystkich przedmiotów, lub do spisu wszystkich otrzymanych przez siebie ocen. Rola studenta w aplikacji ogranicza się głównie do konsumpcji informacji.

## Bezpieczeństwo

Dostęp do wrażliwych danych jest odpowiednio ograniczony. Tylko administrator ma dostęp do wszystkich danych. Nauczyciele mają dostęp tylko do przedmiotów których uczą. Mogą zatem widzieć tylko obecności i oceny które zami wystawili.

Uczniowie mają dostęp jedynie do danych przedmiotów na które chodzą i ocen, które sami otrzymali
