Documentația Aplicației
1. Introducere
Aplicația Rezervări Restaurant este o platformă care permite utilizatorilor să facă rezervări, să comande produse dintr-un meniu și să lase recenzii, iar administratorii să gestioneze meniul, rezervările și recenziile.
1.1. Tehnologii folosite
•	ASP.NET Core MVC – pentru arhitectura aplicației.
•	Entity Framework Core – pentru gestionarea bazei de date.
•	SQL Server – pentru stocarea datelor.
•	ASP.NET Identity – pentru autentificare și autorizare.
•	Bootstrap – pentru interfața utilizatorului.

2. Fluxul Aplicației
•	Utilizator autenticat (client) – poate face rezervări, comenzi și recenzii.
•	Administrator – poate gestiona rezervările, meniul și recenziile.
2.1. Utilizator Neautentificat
•	Poate vedea meniul.
•	Nu poate face rezervări sau comenzi.
•	Dacă încearcă să rezerve o masă sau să lase o recenzie, este redirecționat spre autentificare.
 ________________________________________
2.2. Utilizator Autentificat (Client)
•	Poate face o rezervare prin selectarea datei, orei și numărului de persoane.
•	După rezervare, este redirecționat spre gestionarea comenzii, unde poate adăuga produse.
•	Poate vedea lista rezervărilor sale și edita sau șterge o rezervare (dacă nu a fost aprobată de un admin).
•	Poate lăsa o recenzie, dar nu o poate edita sau șterge.
________________________________________

2.3. Administrator
•	Poate vedea și aproba rezervările.
•	Poate edita meniul (adăuga, șterge sau modifica produse).
•	Poate vedea și șterge toate recenziile, dar nu le poate edita.
•	Poate vedea toate comenzile.
 ________________________________________
3. Structura Bazei de Date
Baza de date a aplicației Rezervări Restaurant este proiectată pentru a gestiona utilizatorii, rezervările, comenzile, produsele din meniu și recenziile acestora. Aceasta conține următoarele tabele:
•	AspNetUsers → Stochează utilizatorii aplicației, atât clienți, cât și administratori. Fiecare utilizator are un identificator unic (Id), un nume de utilizator, email și alte detalii de autentificare gestionate de ASP.NET Identity.
•	AspNetRoles & AspNetUserRoles → Gestionarea rolurilor. Fiecare utilizator poate avea unul sau mai multe roluri (ex: User, Admin). Administratorii au acces la funcționalități speciale, precum aprobarea rezervărilor și gestionarea meniului.
•	Reservations → Conține toate rezervările efectuate de utilizatori. Fiecare rezervare este asociată unui utilizator (IdUser), are o dată și oră (ReservationDate), un număr de persoane (Guests) și un status (Pending, Approved, Cancelled).
•	Orders → O comandă este asociată unei rezervări și este generată automat atunci când un utilizator face o rezervare. Comenzile conțin referința rezervării (IdReservation), iar produsele comandate sunt salvate în OrderDetails.
•	OrderDetails → Această tabelă leagă comenzile de produsele din meniu. Fiecare înregistrare conține un produs (IdMenuItem), cantitatea comandată (Quantity) și este asociată unei comenzi (IdOrder).
•	MenuItems → Reprezintă lista de produse disponibile pentru comandă. Fiecare produs are un nume (Name), o descriere (Description, opțional) și un preț (Price). Administratorii pot adăuga, edita sau șterge produse din meniu.
•	Reviews → Recenziile lăsate de utilizatori după efectuarea unei rezervări. Fiecare recenzie este asociată unui utilizator (IdUser) și conține un scor (Rating, între 1 și 5) și un comentariu (Text). Utilizatorii pot lăsa o singură recenzie per rezervare, iar administratorii pot vedea și șterge recenziile, dar nu le pot edita.
 
3.1. Relațiile dintre tabele
•	Un utilizator poate avea mai multe rezervări.
•	 O rezervare are exact o comandă asociată.
•	 O comandă poate avea mai multe produse.
________________________________________
4. Securitate și Constrângeri
•	Autentificarea și autorizarea sunt gestionate cu ASP.NET Identity.
•	Utilizatorii pot vedea doar propriile rezervări și recenzii, dar administratorii au acces la toate.
•	O rezervare nu poate fi modificată după ce a fost aprobată.
•	Comenzile sunt legate strict de rezervări – nu se pot face fără o rezervare.
 
