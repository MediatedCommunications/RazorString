namespace Example.Templates {
    public record MessageContext {
        public required string Name { get; init; }
        public required DateTime Date_Of_Birth { get; init; }
    }
}
