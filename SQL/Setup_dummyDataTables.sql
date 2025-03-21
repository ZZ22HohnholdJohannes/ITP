USE reserve_it;

INSERT INTO anschrift (strasse, hausnummer, ort, postleitzahl, land)
VALUES
('Hauptstraße', '10', 'Berlin', '10115', 'Deutschland'),
('Musterweg', '5', 'Hamburg', '20095', 'Deutschland'),
('Parkstraße', '14', 'München', '80331', 'Deutschland'),
('Bergstraße', '7', 'Köln', '50733', 'Deutschland'),
('Seestraße', '21', 'Berlin', '13407', 'Deutschland');

INSERT INTO gast (vorname, nachname, geburtsdatum, geschlecht, anschrift_ID, iststammgast)
VALUES
('Max', 'Mustermann', '1985-06-15', 'M', 1, 1),
('Erika', 'Musterfrau', '1990-09-20', 'W', 2, 0),
('Luca', 'Müller', '1995-03-25', 'M', 3, 0),
('Anna', 'Schmidt', '1988-07-30', 'W', 4, 1),
('Paul', 'Weber', '1992-11-12', 'M', 5, 0);

INSERT INTO hotel (anschrift_ID)
VALUES
(1),
(2),
(3),
(4),
(5);

INSERT INTO kategorie (kategorie_ID, kategorie_beschreibung)
VALUES
(1, 'Standard'),
(2, 'Premium'),
(3, 'Luxus');

INSERT INTO art (art_ID, art_beschreibung)
VALUES
(1, 'Einzelzimmer'),
(2, 'Doppelzimmer');

INSERT INTO preis (preis_ID, kategorie_ID, art_ID, preis_num)
VALUES
(1, 1, 1, 80.00),
(2, 1, 2, 120.00),
(3, 2, 1, 180.00),
(4, 2, 2, 250.00),
(5, 3, 1, 500.00),
(6, 3, 2, 600.00);

INSERT INTO hotelzimmer (preis_ID, hotel_ID, zimmernummer)
VALUES
(1, 1, '101'),
(2, 1, '102'),
(3, 2, '201'),
(4, 3, '301'),
(5, 4, '401'),
(6, 4, '403');

INSERT INTO auftrag (gast_ID, startdatum, enddatum)
VALUES
(1, '2024-03-01', '2024-03-05'),
(2, '2024-03-10', '2024-03-15'),
(3, '2024-04-01', '2024-04-07'),
(4, '2024-05-01', '2024-05-05'),
(5, '2024-06-01', '2024-06-05'),
(1, '2024-09-12', '2024-12-12'),
(2, '2024-10-01', '2024-10-11'),
(3, '2024-10-24', '2024-11-02');

INSERT INTO buchung (auftrag_ID, hotelzimmer_ID)
VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 1),
(7, 2),
(8, 3);

INSERT INTO bewertung (auftrag_ID, istfreigegeben, rezension)
VALUES
(1, 1, 'Super Aufenthalt!'),
(2, 0, 'Sehr gut, aber kleines Bad.'),
(3, 1, 'Alles war perfekt, sehr zufrieden!'),
(4, 1, 'Gutes Hotel, jedoch etwas zu teuer.'),
(5, 0, 'Nett, aber die Lage ist nicht ideal.');
