using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Beautyy.Models;

public partial class BeautyContext : DbContext
{
    public BeautyContext()
    {
    }

    public BeautyContext(DbContextOptions<BeautyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Birthdate> Birthdates { get; set; }

    public virtual DbSet<Button> Buttons { get; set; }

    public virtual DbSet<ButtonElement> ButtonElements { get; set; }

    public virtual DbSet<ButtonElementForm> ButtonElementForms { get; set; }

    public virtual DbSet<ButtonForm> ButtonForms { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CombineElement> CombineElements { get; set; }

    public virtual DbSet<CombineFormElementTemplate> CombineFormElementTemplates { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<ComponentElement> ComponentElements { get; set; }

    public virtual DbSet<Container> Containers { get; set; }

    public virtual DbSet<Containing> Containings { get; set; }

    public virtual DbSet<Date> Dates { get; set; }

    public virtual DbSet<DateT> DateTs { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCategory> EventCategories { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<FormCombineElement> FormCombineElements { get; set; }

    public virtual DbSet<FormComponent> FormComponents { get; set; }

    public virtual DbSet<FormComponentTemplate> FormComponentTemplates { get; set; }

    public virtual DbSet<FormElement> FormElements { get; set; }

    public virtual DbSet<FormElementTemplate> FormElementTemplates { get; set; }

    public virtual DbSet<FormInpuTextTemplate> FormInpuTextTemplates { get; set; }

    public virtual DbSet<FormInputDateTemplate> FormInputDateTemplates { get; set; }

    public virtual DbSet<FormInputFileTemplate> FormInputFileTemplates { get; set; }

    public virtual DbSet<FormLabelTemplate> FormLabelTemplates { get; set; }

    public virtual DbSet<FormOptionTemplate> FormOptionTemplates { get; set; }

    public virtual DbSet<FormTemplate> FormTemplates { get; set; }

    public virtual DbSet<GridForImage> GridForImages { get; set; }

    public virtual DbSet<GridTwoColumn> GridTwoColumns { get; set; }

    public virtual DbSet<ImageDesc> ImageDescs { get; set; }

    public virtual DbSet<ImageFile> ImageFiles { get; set; }

    public virtual DbSet<ImageUploadwithImagecontent> ImageUploadwithImagecontents { get; set; }

    public virtual DbSet<ImageWithCaption> ImageWithCaptions { get; set; }

    public virtual DbSet<Imageupload> Imageuploads { get; set; }

    public virtual DbSet<IndyPage> IndyPages { get; set; }

    public virtual DbSet<Number> Numbers { get; set; }

    public virtual DbSet<OneTopicimagecaptionbutton> OneTopicimagecaptionbuttons { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<PictureForm> PictureForms { get; set; }

    public virtual DbSet<Popup> Popups { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Singleselection> Singleselections { get; set; }

    public virtual DbSet<TableWithTopicAndDesc> TableWithTopicAndDescs { get; set; }

    public virtual DbSet<Text> Texts { get; set; }

    public virtual DbSet<TextBox> TextBoxes { get; set; }

    public virtual DbSet<Textfield> Textfields { get; set; }

    public virtual DbSet<TwoTopicimagecaptionbutton> TwoTopicimagecaptionbuttons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=GUYMICKEY; Initial Catalog=beauty; Integrated Security=True; Pooling=False; Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<About>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Banner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Banner_Component");
        });

        modelBuilder.Entity<Birthdate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Birthdate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Birthdate_FormTemplate");
        });

        modelBuilder.Entity<Button>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Button)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Button_Component");
        });

        modelBuilder.Entity<ButtonElement>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ButtonElement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ButtonElement_ComponentElement");
        });

        modelBuilder.Entity<ButtonElementForm>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ButtonElementForm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ButtonElementForm_FormElementTemplate");
        });

        modelBuilder.Entity<ButtonForm>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ButtonForm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ButtonForm_FormTemplate");
        });

        modelBuilder.Entity<CombineElement>(entity =>
        {
            entity.HasOne(d => d.ComponentElement).WithMany(p => p.CombineElements).HasConstraintName("FK_CombineElement_ComponentElement");

            entity.HasOne(d => d.Component).WithMany(p => p.CombineElements).HasConstraintName("FK_CombineElement_Component");
        });

        modelBuilder.Entity<CombineFormElementTemplate>(entity =>
        {
            entity.HasOne(d => d.FormComponent).WithMany(p => p.CombineFormElementTemplates).HasConstraintName("FK_CombineFormElementTemplate_FormComponentTemplate");

            entity.HasOne(d => d.FormElement).WithMany(p => p.CombineFormElementTemplates).HasConstraintName("FK_CombineFormElementTemplate_FormElementTemplate");
        });

        modelBuilder.Entity<Container>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Container)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Container_Component");
        });

        modelBuilder.Entity<Containing>(entity =>
        {
            entity.HasOne(d => d.Component).WithMany(p => p.Containings).HasConstraintName("FK_Containing_Component");

            entity.HasOne(d => d.Container).WithMany(p => p.Containings).HasConstraintName("FK_Containing_Container");
        });

        modelBuilder.Entity<Date>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Date)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Date_FormTemplate");
        });

        modelBuilder.Entity<DateT>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DateTime");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.DateT)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DateTime_ComponentElement");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasOne(d => d.Image).WithMany(p => p.Events).HasConstraintName("FK_Event_ImageFile");
        });

        modelBuilder.Entity<EventCategory>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.EventCategories).HasConstraintName("FK_EventCategory_Category");

            entity.HasOne(d => d.Event).WithMany(p => p.EventCategories).HasConstraintName("FK_EventCategory_Event");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasOne(d => d.FormTemplate).WithMany(p => p.Forms).HasConstraintName("FK_Form_FormTemplate");
        });

        modelBuilder.Entity<FormCombineElement>(entity =>
        {
            entity.HasOne(d => d.FormCompoent).WithMany(p => p.FormCombineElements).HasConstraintName("FK_FormCombineElement_FormComponent");

            entity.HasOne(d => d.FormElement).WithMany(p => p.FormCombineElements).HasConstraintName("FK_FormCombineElement_FormElement");
        });

        modelBuilder.Entity<FormComponent>(entity =>
        {
            entity.HasOne(d => d.FormComponentTemplate).WithMany(p => p.FormComponents).HasConstraintName("FK_FormComponent_FormComponentTemplate");

            entity.HasOne(d => d.Form).WithMany(p => p.FormComponents).HasConstraintName("FK_FormComponent_Form");
        });

        modelBuilder.Entity<FormComponentTemplate>(entity =>
        {
            entity.HasOne(d => d.Form).WithMany(p => p.FormComponentTemplates).HasConstraintName("FK_FormComponentTemplate_FormTemplate");
        });

        modelBuilder.Entity<FormElement>(entity =>
        {
            entity.HasOne(d => d.FormElementTeplate).WithMany(p => p.FormElements).HasConstraintName("FK_FormElement_FormElementTemplate");
        });

        modelBuilder.Entity<FormInpuTextTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.FormInpuTextTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormInpuTextTemplate_FormElementTemplate");
        });

        modelBuilder.Entity<FormInputDateTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.FormInputDateTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormInputDateTemplate_FormElementTemplate");
        });

        modelBuilder.Entity<FormInputFileTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.FormInputFileTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormInputFileTemplate_FormElementTemplate");
        });

        modelBuilder.Entity<FormLabelTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.FormLabelTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormLabelTemplate_FormElementTemplate");
        });

        modelBuilder.Entity<FormOptionTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.FormOptionTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormOptionTemplate_FormElementTemplate");
        });

        modelBuilder.Entity<FormTemplate>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.FormTemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormTemplate_Component");
        });

        modelBuilder.Entity<GridForImage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.GridForImage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GridForImage_Component");
        });

        modelBuilder.Entity<GridTwoColumn>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.GridTwoColumn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GridTwoColumn_Component");
        });

        modelBuilder.Entity<ImageDesc>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ImageDesc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImageDesc_Component");
        });

        modelBuilder.Entity<ImageUploadwithImagecontent>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ImageUploadwithImagecontent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImageUploadwithImagecontent_FormTemplate");
        });

        modelBuilder.Entity<ImageWithCaption>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ImageWithCaption)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImageWithCaption_Component");
        });

        modelBuilder.Entity<Imageupload>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Imageupload)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Imageupload_FormTemplate");
        });

        modelBuilder.Entity<IndyPage>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.IndyPage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndyPage_Container");
        });

        modelBuilder.Entity<Number>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Number)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Number_ComponentElement");
        });

        modelBuilder.Entity<OneTopicimagecaptionbutton>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.OneTopicimagecaptionbutton)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OneTopicimagecaptionbutton_Component");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Event).WithMany(p => p.Pages).HasConstraintName("FK_Page_Event");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Page)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Page_Container");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Picture)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Picture_ComponentElement");

            entity.HasOne(d => d.Image).WithMany(p => p.Pictures).HasConstraintName("FK_Picture_ImageFile");
        });

        modelBuilder.Entity<PictureForm>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.PictureForm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PictureForm_FormElementTemplate");

            entity.HasOne(d => d.Image).WithMany(p => p.PictureForms).HasConstraintName("FK_PictureForm_ImageFile");
        });

        modelBuilder.Entity<Popup>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Popup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Popup_FormComponentTemplate");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Sale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sale_Component");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Section)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Section_Container");
        });

        modelBuilder.Entity<Singleselection>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Singleselection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Singleselection_FormTemplate");
        });

        modelBuilder.Entity<TableWithTopicAndDesc>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TableWithTopicAndDesc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableWithTopicAndDesc_Component");
        });

        modelBuilder.Entity<Text>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Text)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Text_ComponentElement");
        });

        modelBuilder.Entity<TextBox>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TextBox)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TextBox_Component");
        });

        modelBuilder.Entity<Textfield>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Textfield)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Textfield_FormTemplate");
        });

        modelBuilder.Entity<TwoTopicimagecaptionbutton>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.TwoTopicimagecaptionbutton)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TwoTopicimagecaptionbutton_Component");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
