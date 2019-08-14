- Core: IResourcTypeeNotificationTemplateModel : ¿Hay una ´e´ repetida?
- Kernel: ICanBePurged => ¿Repetido en Kernel.Statuses?

    /*  GetHashCode */

    // Kernel:PropertyBagType
    public override int GetHashCode()
{
    var hashCode = -789346321;
    hashCode = hashCode * -1521134295 + IsSingleton.GetHashCode();
    hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
    return hashCode;
}

// Domain:ValueObject
public override int GetHashCode()
{
    unchecked
    {
        var hashCode = ProvisioningTaskId;
        hashCode = (hashCode * 397) ^ (int)Status;
        hashCode = (hashCode * 397) ^ (Provisioning != null ? Provisioning.GetHashCode() : 0);
        return hashCode;
    }
}

public override int GetHashCode()
{
    unchecked
    {
        return ((AppCommandId != null ? AppCommandId.GetHashCode() : 0) * 397)
               ^ (WorkflowDefinitionId != null ? WorkflowDefinitionId.GetHashCode() : 0);
    }
}

// ConsoleCommand:isCommand
public override int GetHashCode()
{
    unchecked
    {
        var hashCode = base.GetHashCode();
        hashCode = (hashCode * 397) ^ (CommandName != null ? CommandName.ToLowerInvariant().GetHashCode() : 0);
        return hashCode;
    }
}

// Core:Collection:FallbackDictionary
public override int GetHashCode()
{
    unchecked
    {
        var hashCode = 13;

        foreach (var part in _parts)
        {
            hashCode = hashCode * 23 + part.GetHashCode();
        }
        return hashCode;
    }
}

// Kernel:EntityIdBase
public override int GetHashCode()
{
    return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
}

// Kernel:EntityIdBase 
public override int GetHashCode()
{
    var hashCode = 1978346239;
    hashCode = hashCode * -1521134295 + base.GetHashCode();
    hashCode = hashCode * -1521134295 + (Property1 != null ? Property1.GetHashCode() : 0);
    hashCode = hashCode * -1521134295 + Guid.GetHashCode();
    return hashCode;
}

// Infraestructure:RequestNewNetworkResourceCommandJsonConverterTests
public override int GetHashCode()
{
    return -832886068 + EqualityComparer<IUserPrincipalId>.Default.GetHashCode(PrincipalId);
}

// Core:AdPrincipalId
public override int GetHashCode()
{
    unchecked
    {
        if (AdGuid != Guid.Empty)
        {
            return AdGuid.GetHashCode();
        }

        if (!string.IsNullOrEmpty(DistinguishedName))
        {
            return DistinguishedName.GetHashCode();
        }

        var hashCode = (Domain != null ? _domain.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (SamAccountName != null ? SamAccountName.GetHashCode() : 0);
        return hashCode;
    }
}

// Core:RabbitAdAccountPrincipalId
public override int GetHashCode()
{
    var hashCode = -626001325;
    hashCode = (hashCode * -1521134295) + (string.IsNullOrWhiteSpace(Id) ? 0 : Id.GetHashCode());
    hashCode = (hashCode * -1521134295) + (string.IsNullOrWhiteSpace(AccountId) ? 0 : AccountId.GetHashCode());
    hashCode = (hashCode * -1521134295) + AdGuid.GetHashCode();
    hashCode = (hashCode * -1521134295) + (string.IsNullOrWhiteSpace(DistinguishedName) ? 0 : DistinguishedName.GetHashCode());
    hashCode = (hashCode * -1521134295) + (string.IsNullOrWhiteSpace(Domain) ? 0 : Domain.GetHashCode());
    hashCode = (hashCode * -1521134295) + (string.IsNullOrWhiteSpace(SamAccountName) ? 0 : SamAccountName.GetHashCode());
    return hashCode;
}

// Kernel:AppCommandAuthorizationControlEntryBase
public override int GetHashCode()
{
    return (CommandId != null ? CommandId.GetHashCode() : 0) ^
           DerivedHashCode();
}

// Kernel:Authorization:AppCommandAuthorizationControlEntry

protected override int DerivedHashCode()
{
    //Base implementation is enough
    return 1;
}

// Kernel:Authorization:ChangePropertiesAuthorizationControlEntry
protected override int DerivedHashCode()
{
    unchecked
    {
        var hashCode = 397;
        hashCode = (hashCode * 397) ^ (ConfigurationTypeId != null ? ConfigurationTypeId.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (PropertyBagType != null ? PropertyBagType.GetHashCode() : 0);
        foreach (var property in PropertiesToChange)
            hashCode = (hashCode * 397) ^ (property?.GetHashCode() ?? 0);
        return hashCode;
    }
}

// Kernel:Scope:AnyScope
public override int GetHashCode()
{
    return "AnyObect".GetHashCode();
}

// Kernel:CommandType
public override int GetHashCode()
{
    return AppCommandId.GetHashCode() ^ Type.GetHashCode();
}

// UI.Web.HelpPageSampleKey
public override int GetHashCode()
{
    var hashCode = ControllerName.ToUpperInvariant().GetHashCode() ^ ActionName.ToUpperInvariant().GetHashCode();

    if (MediaType != null)
    {
        hashCode ^= MediaType.GetHashCode();
    }
    if (SampleDirection != null)
    {
        hashCode ^= SampleDirection.GetHashCode();
    }
    if (ParameterType != null)
    {
        hashCode ^= ParameterType.GetHashCode();
    }
    foreach (var parameterName in ParameterNames)
    {
        hashCode ^= parameterName.ToUpperInvariant().GetHashCode();
    }
    return hashCode;
}

/* DUDA: En el Equals es InvariantCulture y en GetHashCode NO ¿Está Ok?*/

// Infraestructure:SearchItemViewModel
protected bool Equals(SearchItemViewModel other)
{
    return string.Equals(Id, other.Id, StringComparison.InvariantCultureIgnoreCase)
           && string.Equals(SubTypeName, other.SubTypeName, StringComparison.InvariantCultureIgnoreCase)
           && EntityType == other.EntityType;
}

public override bool Equals(object obj)
{
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != this.GetType()) return false;
    return Equals((SearchItemViewModel)obj);
}

public override int GetHashCode()
{
    unchecked
    {
        var hashCode = (Id != null ? Id.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (SubTypeName != null ? SubTypeName.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (int)EntityType;
        return hashCode;
    }
}
	}

	/* Equals */
	//C:\_algacom\Rabbit\src\Algacom.Rabbit.Configuration\RelationTypes\RelationType.cs
	public override bool Equals(RelationType other)
{
    if (ReferenceEquals(null, other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return base.Equals(other)
            && string.Equals(RelationCustomInfoTypeName, other.RelationCustomInfoTypeName)
            && DeleteBehaviour == other.DeleteBehaviour;
}

public override bool Equals(object obj)
{
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != GetType()) return false;
    return Equals((RelationType)obj);
}