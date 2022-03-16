package com.irinushirka.users;


public class UserNotFoundException extends Exception {
    private long book_id;
    public UserNotFoundException(long book_id) {
        super(String.format("Not found with id : '%s'", book_id));
    }
}