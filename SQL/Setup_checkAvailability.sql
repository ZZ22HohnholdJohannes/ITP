USE reserve_it;

DELIMITER //
CREATE PROCEDURE checkAvailability(IN startDate date, IN endDate DATE, IN kategorieZimmer INT, IN artZimmer INT) 
BEGIN

# Gebe alle Zimmer aus, die die angegebene Kategorie und Art haben und deren gebuchter Zeitraum nicht mit dem angegebenen Zeitraum überschneidet

    SELECT k.kategorie_beschreibung AS kategorie,
           a.art_beschreibung AS zimmerart,
           p.preis_num AS preis_pro_nacht
    FROM hotelzimmer hz
    LEFT JOIN preis p ON hz.preis_ID = p.preis_ID
    LEFT JOIN art a ON p.art_ID = a.art_ID
    LEFT JOIN kategorie k ON k.kategorie_ID = p.kategorie_ID
    WHERE k.kategorie_ID = kategorieZimmer
      AND a.art_ID = artZimmer
      AND NOT EXISTS (
          SELECT 1 
          FROM buchung b
          JOIN auftrag auf ON b.auftrag_ID = auf.auftrag_ID
          WHERE b.hotelzimmer_ID = hz.hotelzimmer_ID
            AND NOT (auf.enddatum <= startDate OR endDate <= auf.startdatum)
      )
    LIMIT 1;
	       
END //
DELIMITER ;
