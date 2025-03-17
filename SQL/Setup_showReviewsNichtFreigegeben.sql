USE reserve_it;

DELIMITER //
CREATE PROCEDURE showReviewsNichtFreigegeben()
BEGIN 
	
	SELECT *
	FROM bewertung
	WHERE istFreigegeben = 0;
	
END//
DELIMITER ;