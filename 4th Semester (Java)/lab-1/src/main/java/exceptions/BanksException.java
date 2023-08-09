package exceptions;

/**
 * Custom exception for Banks lab.
 */
public class BanksException extends RuntimeException {
    /**
     * BanksException constructor.
     * @param message exceptions message
     */
    public BanksException(String message) {
        super(message);
    }
}