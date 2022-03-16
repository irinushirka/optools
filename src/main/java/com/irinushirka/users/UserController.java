package com.irinushirka.users;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
//import javax.validation.Valid;
import java.util.List;



@RestController
public class UserController {

    @Autowired
    UserRepository userRepository;

    // Get All Notes
    @GetMapping("/users")
    public List<User> getAllNotes() {
        return userRepository.findAll();
    }

    @GetMapping("/users/blockUser/{id}")
    public Boolean getNoteById(@PathVariable(value = "id") Integer userId) throws UserNotFoundException {
        User user = userRepository.findById(userId)
                .orElseThrow(() -> new UserNotFoundException(userId));

        user.setIs_blocked(!user.getIs_blocked());
        userRepository.save(user);
        return true;
               // .orElseThrow(() -> new BookNotFoundException(userId));
    }

    @PostMapping("/login")
    public User loginUser(@RequestBody LoginRequest request) throws UserNotFoundException {
        List<User> users = userRepository.findByLogin(request.getLogin());
        User user =  users.stream().findFirst().orElseThrow(() -> new UserNotFoundException(1));
        System.out.println(user.getPassword());
        System.out.println(request.getPassword());
        if (request.getPassword().equals(user.getPassword())) {
            return user;
        } else {
            throw new UserNotFoundException(2);
        }
    }

    @DeleteMapping("/users/delete/{id}")
    public Boolean deleteBook(@PathVariable(value = "id") Integer bookId) throws UserNotFoundException {
        User book = userRepository.findById(bookId)
                .orElseThrow(() -> new UserNotFoundException(bookId));

        userRepository.delete(book);

        return true;
    }


    @PostMapping("/register")
    public User createNote(@RequestBody User user) {
        return userRepository.save(user);
    }
}