namespace Cogax.DependencyInjectionExtensions.Example
{
    public enum AuftragStatus
    {
        Erfasst,
        Bearbeitet,
        Deaktiviert,
        Archiviert,
        Geloescht
    }

    public class Auftrag
    {
        public AuftragStatus Status { get; set; }

        // ...
    }
}
