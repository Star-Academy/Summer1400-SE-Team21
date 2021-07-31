public class ConsoleWriter implements Writer{
    @Override
    public void write(String output) {
        System.out.println(output);
    }
}
