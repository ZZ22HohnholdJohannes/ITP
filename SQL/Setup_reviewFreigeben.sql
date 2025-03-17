USE reserve_it;

DELIMITER //
CREATE PROCEDURE reviewFreigeben (IN auftrag_id_in INT)
BEGIN

	UPDATE bewertung
	SET istFreigegeben = 'true'
	WHERE auftrag_ID = auftrag_id_int;
	
END//
DELIMITER ;