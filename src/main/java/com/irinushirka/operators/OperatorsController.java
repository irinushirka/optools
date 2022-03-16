package com.irinushirka.operators;
import com.irinushirka.users.UserNotFoundException;
import org.springframework.beans.factory.annotation.Autowired;
        import org.springframework.web.bind.annotation.*;

        import java.util.List;

@RestController
public class OperatorsController {

    @Autowired
    OperatorsRepository operatorsRepository;

    // Get All Notes
    @GetMapping("/operators")
    public List<Operators> getAllNotes() {
        return operatorsRepository.findAll();
    }

    @PostMapping("/operators")
    public Operators createNote(@RequestBody Operators operator) {
        return operatorsRepository.save(operator);
    }

    @DeleteMapping("/operators/{id}")
    public Boolean deleteOperator(@PathVariable(value = "id") Integer opId) throws UserNotFoundException {
        Operators oper = operatorsRepository.findById(opId)
                .orElseThrow(() -> new UserNotFoundException(opId));

        operatorsRepository.delete(oper);

        return true;//ResponseEntity.ok().build();
    }

    @PutMapping("/operators/{id}")
    public Operators updateNote(@PathVariable(value = "id") Integer operatorId,
                                @RequestBody Operators operatorDetails) throws UserNotFoundException {

        Operators operator = operatorsRepository.findById(operatorId)
                .orElseThrow(() -> new UserNotFoundException(operatorId));

        operator.setFirst_name(operatorDetails.getFirst_name());
        operator.setAccount(operatorDetails.getAccount());
        operator.setLast_name(operatorDetails.getLast_name());
        operator.setSecond_name(operatorDetails.getSecond_name());
        operator.setAccumulated_tenure(operatorDetails.getAccumulated_tenure());

        Operators updatedOperator = operatorsRepository.save(operator);

        return updatedOperator;
    }
}