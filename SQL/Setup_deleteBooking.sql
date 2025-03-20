USE reserve_it;

DELIMITER //

CREATE PROCEDURE deleteBooking(IN auftrag_id_in INT)
BEGIN

# Deletes entry with the auftrag_ID of a specific order in the tables buchung, bewertung and auftrag
	
	DELETE FROM buchung 
	WHERE auftrag_ID = auftrag_id_in;
	
	DELETE FROM bewertung 
	WHERE auftrag_ID = auftrag_id_in;
	
	DELETE FROM auftrag 
	WHERE auftrag_ID = auftrag_id_in;
	
END//
DELIMITER ;