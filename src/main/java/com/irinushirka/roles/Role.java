package com.irinushirka.roles;

import java.io.Serializable;
import javax.persistence.*;

@Entity
@Table(name = "roles")
public class Role implements Serializable  {
    @Id
    @GeneratedValue
    public Integer role_id;
    public String role_name;
    public Role() {}
}
