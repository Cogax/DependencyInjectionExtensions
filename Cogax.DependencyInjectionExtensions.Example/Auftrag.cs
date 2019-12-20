namespace Cogax.DependencyInjectionExtensions.Example
{
    public class Auftrag
    {
        public AuftragStatus Status { get; set; }

        // ...
    }

    public enum AuftragStatus
    {
        Erfasst,
        Bearbeitet,
        Deaktiviert,
        Archiviert,
        Geloescht
    }
}
