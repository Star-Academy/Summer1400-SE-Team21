import org.junit.jupiter.api.Test;

import java.util.Arrays;
import java.util.TreeSet;

import static org.junit.jupiter.api.Assertions.*;

class UserInputTest {
    @Test
    public void processUserString(){
        String input = "hello this +fine -cats";
        UserInput userInput = new UserInput(input);

        TreeSet<String> expectedAndInput = new TreeSet<>(Arrays.asList("hello"));
        TreeSet<String> expectedOrInput = new TreeSet<>(Arrays.asList("fine"));
        TreeSet<String> expectedRemoveInput = new TreeSet<>(Arrays.asList("cat"));

        assertEquals(expectedAndInput,userInput.getAndInputs());
        assertEquals(expectedOrInput,userInput.getOrInputs());
        assertEquals(expectedRemoveInput,userInput.getRemoveInputs());
    }
}