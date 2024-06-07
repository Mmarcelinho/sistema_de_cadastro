namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Mappings;

    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            // Configurações básicas
            builder.ToTable("Pessoas");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(p => p.Cnpj)
                .HasMaxLength(14);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.NomeFantasia)
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .HasMaxLength(255);

            builder.Property(p => p.Nascimento)
                .IsRequired();

            builder.Property(p => p.Token)
                .HasMaxLength(255);

            builder.HasOne(p => p.Cadastro)
                .WithMany()
                .HasForeignKey(p => p.CadastroId)
                .IsRequired();

            builder.OwnsMany(p => p.Domicilios, navigationBuilder =>
            {
                navigationBuilder.ToTable("Domicilios");
                navigationBuilder.WithOwner().HasForeignKey("PessoaId");
                navigationBuilder.Property<long>("Id");
                navigationBuilder.HasKey("Id");

                navigationBuilder.Property(d => d.Tipo).HasColumnName("DomicilioTipo").IsRequired();
                navigationBuilder.OwnsOne(d => d.Endereco, enderecoBuilder =>
                {
                    enderecoBuilder.Property(e => e.Cep).HasColumnName("Cep").HasMaxLength(8).IsRequired();
                    enderecoBuilder.Property(e => e.Logradouro).HasColumnName("Logradouro").HasMaxLength(255).IsRequired();
                    enderecoBuilder.Property(e => e.Numero).HasColumnName("Numero").HasMaxLength(10).IsRequired();
                    enderecoBuilder.Property(e => e.Bairro).HasColumnName("Bairro").HasMaxLength(50).IsRequired();
                    enderecoBuilder.Property(e => e.Complemento).HasColumnName("Complemento").HasMaxLength(255);
                    enderecoBuilder.Property(e => e.PontoReferencia).HasColumnName("PontoReferencia").HasMaxLength(255);
                    enderecoBuilder.Property(e => e.UF).HasColumnName("UF").HasMaxLength(2).IsRequired();
                    enderecoBuilder.Property(e => e.Cidade).HasColumnName("Cidade").HasMaxLength(50).IsRequired();
                    enderecoBuilder.Property(e => e.Ibge).HasColumnName("Ibge").IsRequired();
                });
            });

            builder.OwnsOne(p => p.Telefone, telefoneBuilder =>
            {
                telefoneBuilder.Property(t => t.Numero).HasColumnName("TelefoneNumero").IsRequired();
                telefoneBuilder.Property(t => t.Celular).HasColumnName("TelefoneCelular").IsRequired();
                telefoneBuilder.Property(t => t.Whatsapp).HasColumnName("TelefoneWhatsapp").IsRequired();
                telefoneBuilder.Property(t => t.Telegram).HasColumnName("TelefoneTelegram").IsRequired();
            });
        }
    }

