using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Beautyy.Models
{
    public partial class CustomContext : BeautyContext //CustomContext เป็นคลาส context ย่อย ที่สืบทอดจาก TestBgContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Component>().UseTptMappingStrategy().ToTable("Component");
            //TPT หมายความว่า: class แม่ (Component) และ class ลูก (เช่น Container, Banner) จะมี table แยกของตัวเอง และใช้ JOIN เพื่อดึงข้อมูล
            modelBuilder.Entity<Banner>().ToTable("Banner");
            modelBuilder.Entity<TextBox>().ToTable("TextBox");
            modelBuilder.Entity<ImageWithCaption>().ToTable("ImageWithCaption");
            modelBuilder.Entity<GridTwoColumn>().ToTable("GridTwoColumn");
            modelBuilder.Entity<ImageDesc>().ToTable("ImageDesc");
            modelBuilder.Entity<GridForImage>().ToTable("GridForImage");
            modelBuilder.Entity<TableWithTopicAndDesc>().ToTable("Tablewithtopicanddesc");
            modelBuilder.Entity<OneTopicimagecaptionbutton>().ToTable("OneTopicimagecaptionbutton");
            modelBuilder.Entity<TwoTopicimagecaptionbutton>().ToTable("TwoTopicimagecaptionbutton");
            modelBuilder.Entity<Sale>().ToTable("Sale");
            modelBuilder.Entity<Button>().ToTable("Button");
            modelBuilder.Entity<About>().ToTable("About");
            modelBuilder.Entity<FormTemplate>().ToTable("FormTemplate");

            modelBuilder.Entity<FormComponentTemplate>().UseTptMappingStrategy().ToTable("FormComponentTemplate");
            modelBuilder.Entity<Singleselection>().ToTable("Singleselection");
            modelBuilder.Entity<Textfield>().ToTable("Textfield");
            modelBuilder.Entity<Date>().ToTable("Date");
            modelBuilder.Entity<Birthdate>().ToTable("Birthdate");
            modelBuilder.Entity<Imageupload>().ToTable("Imageupload");
            modelBuilder.Entity<ImageUploadwithImagecontent>().ToTable("ImageUploadwithImagecontent");
            modelBuilder.Entity<ButtonForm>().ToTable("ButtonForm");
            modelBuilder.Entity<Popup>().ToTable("Popup");

            modelBuilder.Entity<Container>().UseTptMappingStrategy().ToTable("Container");
            modelBuilder.Entity<Container>().Ignore(p => p.Page);
            modelBuilder.Entity<Container>().Ignore(p => p.Section);
            modelBuilder.Entity<Container>().Ignore(i => i.IndyPage);
            modelBuilder.Entity<Page>().ToTable("Page");
            modelBuilder.Entity<Section>().ToTable("Section");
            modelBuilder.Entity<IndyPage>().ToTable("IndyPage");

            modelBuilder.Entity<ComponentElement>().UseTptMappingStrategy().ToTable("ComponentElement");
            modelBuilder.Entity<Button>().ToTable("Button");
            modelBuilder.Entity<Text>().ToTable("Text");
            modelBuilder.Entity<Picture>().ToTable("Picture");
            modelBuilder.Entity<Number>().ToTable("Number");
            modelBuilder.Entity<DateT>().ToTable("DateT");
  

            modelBuilder.Entity<FormElementTemplate>().UseTptMappingStrategy().ToTable("FormElementTemplate");

            modelBuilder.Entity<FormLabelTemplate>().ToTable("FormLabelTemplate");

            modelBuilder.Entity<FormOptionTemplate>().ToTable("FormOptionTemplate");
       
            modelBuilder.Entity<FormInputDateTemplate>().ToTable("FormInputDateTemplate");
          
            modelBuilder.Entity<FormInpuTextTemplate>().ToTable("FormInpuTextTemplate");
      
            modelBuilder.Entity<FormInputFileTemplate>().ToTable("FormInputFileTemplate");

            modelBuilder.Entity<ButtonElementForm>().ToTable("ButtonElementForm");

            modelBuilder.Entity<PictureForm>().ToTable("PictureForm");



            modelBuilder.Entity<Containing>(entity =>
            {
                entity.Ignore(C => C.Container);

                entity.HasOne(d => d.Component).WithMany(p => p.Containings).HasConstraintName("FK_Containing_Component");
            });

            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
