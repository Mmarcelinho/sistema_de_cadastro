namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Mappings;

    public class CadastroMap : IEntityTypeConfiguration<Cadastro>
    {
        public void Configure(EntityTypeBuilder<Cadastro> builder)
        {
            builder.ToTable("Cadastros");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.NomeFantasia)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.SobrenomeSocial)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Empresa)
                .IsRequired();

            builder.OwnsOne(c => c.Credencial, navigationBuilder =>
            {
                navigationBuilder.Property(c => c.Bloqueada).HasColumnName("CredencialBloqueada").IsRequired();
                navigationBuilder.Property(c => c.Expirada).HasColumnName("CredencialExpirada").IsRequired().HasMaxLength(255);
                navigationBuilder.Property(c => c.Senha).HasColumnName("CredencialSenha").IsRequired().HasMaxLength(255);
            });

            builder.OwnsOne(c => c.Inscrito, navigationBuilder =>
            {
                navigationBuilder.Property(i => i.Assinante).HasColumnName("InscritoAssinante").IsRequired();
                navigationBuilder.Property(i => i.Associado).HasColumnName("InscritoAssociado").IsRequired();
                navigationBuilder.Property(i => i.Senha).HasColumnName("InscritoSenha").IsRequired().HasMaxLength(255);
            });

            builder.OwnsOne(c => c.Parceiro, navigationBuilder =>
            {
                navigationBuilder.Property(p => p.Cliente).HasColumnName("ParceiroCliente").IsRequired();
                navigationBuilder.Property(p => p.Fornecedor).HasColumnName("ParceiroFornecedor").IsRequired();
                navigationBuilder.Property(p => p.Prestador).HasColumnName("ParceiroPrestador").IsRequired();
                navigationBuilder.Property(p => p.Colaborador).HasColumnName("ParceiroColaborador").IsRequired();
            });

            builder.OwnsOne(c => c.Documento, navigationBuilder =>
            {
                navigationBuilder.Property(d => d.Numero).HasColumnName("DocumentoNumero").IsRequired().HasMaxLength(50);
                navigationBuilder.Property(d => d.OrgaoEmissor).HasColumnName("DocumentoOrgaoEmissor").IsRequired().HasMaxLength(100);
                navigationBuilder.Property(d => d.EstadoEmissor).HasColumnName("DocumentoEstadoEmissor").IsRequired().HasMaxLength(50);
                navigationBuilder.Property(d => d.DataValidade).HasColumnName("DocumentoDataValidade").IsRequired();
            });

            builder.OwnsOne(c => c.Identificador, navigationBuilder =>
            {
                navigationBuilder.Property(i => i.Empresa).HasColumnName("IdentificacaoEmpresa").IsRequired();
                navigationBuilder.Property(i => i.Identificador).HasColumnName("IdentificacaoIdentificador").IsRequired().HasMaxLength(255);
                navigationBuilder.Property(i => i.Tipo).HasColumnName("IdentificacaoTipo").IsRequired();
            });
        }
    }

