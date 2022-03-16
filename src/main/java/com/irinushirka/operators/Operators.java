package com.irinushirka.operators;


import com.irinushirka.users.User;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.io.Serializable;
import java.lang.reflect.Field;

@Entity
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "operators")
@Data
@Builder
public class Operators implements Serializable {
    @Id
    @GeneratedValue
    public Integer operator_id;

    public String first_name;
    public String second_name;
    public String last_name;
    public String accumulated_tenure;


    @OneToOne(cascade = CascadeType.REFRESH)
    @JoinColumn(name = "account_id", referencedColumnName = "user_id")
    public User account;


    public String toString() {
        StringBuilder result = new StringBuilder();
        String newLine = System.getProperty("line.separator");

        result.append( this.getClass().getName() );
        result.append( " Object {" );
        result.append(newLine);

        //determine fields declared in this class only (no fields of superclass)
        Field[] fields = this.getClass().getDeclaredFields();

        //print field names paired with their values
        for ( Field field : fields  ) {
            result.append("  ");
            try {
                result.append( field.getName() );
                result.append(": ");
                result.append( field.get(this) );
            } catch ( IllegalAccessException ex ) {
                System.out.println(ex);
            }
            result.append(newLine);
        }
        result.append("}");

        return result.toString();
    }
}
