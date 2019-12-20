namespace Cogax.DependencyInjectionExtensions.Bindings
{
    public class BindingMetadata<TConstraint>
    {
        public TConstraint Constraint { get; set; }

        public BindingMetadata(TConstraint constraint)
        {
            Constraint = constraint;
        }

        public bool Matches(BindingMetadata<TConstraint> requestedBindingMetadata)
        {
            return Constraint.Equals(requestedBindingMetadata.Constraint);
        }
    }
}
