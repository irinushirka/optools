package com.irinushirka.roles;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
//import javax.validation.Valid;
import java.util.List;



@RestController
public class RoleController {

    @Autowired
    RoleRepository roleRepository;

    // Get All Notes
    @GetMapping("/roles")
    public List<Role> getAllNotes() {
        return roleRepository.findAll();
    }

    @PostMapping("/roles")
    public Role createNote(@RequestBody Role role) {
        return roleRepository.save(role);
    }
}