USE reserve_it;

DELIMITER //
CREATE PROCEDURE reviewLoeschen (IN auftrag_id_in INT)
BEGIN

	DELETE
	FROM auftrag
	WHERE auftrag_ID = auftrag_id_in;

END//
DELIMITER ;
