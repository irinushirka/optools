package com.irinushirka.users;


import com.irinushirka.roles.Role;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.io.Serializable;

@Entity
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "users")
@Data
@Builder
public class User implements Serializable{
    @Id
    @GeneratedValue
    private int user_id;
    private String login;
    private String password;
    private String email;
    private Boolean is_blocked;

    public String getPassword() {
        return password;
    }

    @OneToOne(cascade = CascadeType.REFRESH)
    @JoinColumn(name = "role_id", referencedColumnName = "role_id")
    private Role role;
}
