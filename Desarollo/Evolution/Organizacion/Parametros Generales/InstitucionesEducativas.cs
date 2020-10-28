using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Shared;
using OpenQA.Selenium.Interactions;


namespace EvolutionAutomationTests
{

	[TestFixture]
	public class InstitucionesEductivas
	{

		IWebDriver driver;
		WebDriverWait wait;
		Actions actions;
		string txtNombreExpected, txtNomCortoExpected, txtPaisExpected;

		[OneTimeSetUp]
		public void SetUp()
		{
			//Inicialización de variables
			CommonLogic commonLogic = new CommonLogic();
			driver = WebDriverSingleton.GetInstance();
			actions = new Actions(driver);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
			txtNombreExpected = "InstituciónEducativa";
            txtNomCortoExpected = "InstituciónEducativa";
            txtPaisExpected = "gt";

            //Ir a pantalla de Instituciones Educativas
            IWebElement applicacion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menucenter']/a[2]")));
			applicacion.Click();
			IWebElement modulo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/table[4]/tbody/tr/td/div[1]/div[2]/h2/a")));
			modulo.Click();
			IWebElement seccion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/div[2]/fieldset/div/div[4]/div[1]/div[2]/h2/a")));
			seccion.Click();
			//IWebElement opcion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/div/div[1]/div[1]/div[2]/h2/a")));
		}

		[Test, Order(1)]
		public void Crear_Institucion_Educativa_Con_Informacion_Basica()
		{
			//Crear nueva institucion educativa
			IWebElement btnNuevo = driver.FindElement(By.Id("smlInstitucionesEducativas_new"));
			btnNuevo.Click();
			IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
			txtNombre.SendKeys(txtNombreExpected);
            IWebElement txtNombreCorto = driver.FindElement(By.Id("NombreCorto"));
            txtNombreCorto.SendKeys(txtNombreExpected);
            IWebElement cmbPais = driver.FindElement(By.Id("codigoPais"));
            cmbPais.Click();
            IWebElement paisGuatemala = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/form/div[1]/fieldset/div[4]/select/option[2]"));
            paisGuatemala.Click();
			IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarInstitucionEducativa")));
			btnGuardar.Click();

			//Editar institucion educativa creada
			IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlInstitucionesEducativas_txtQuickSearch")));
			txtBuscar.SendKeys(txtNombreExpected);
			txtBuscar.SendKeys(Keys.Enter);
			txtBuscar.SendKeys(Keys.Enter);
			IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlInstitucionesEducativas_grid']/div[3]/table/tbody/tr")));
			actions.DoubleClick(tblRegistro).Perform();

			//Verificaciones
			IWebElement txtNombreActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Nombre")));
			string txtNombreActualTexto = txtNombreActual.GetAttribute("value");
			Assert.AreEqual(txtNombreExpected, txtNombreActualTexto);

            IWebElement txtNomCortoActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("NombreCorto")));
            string txtNomCortoActualTexto = txtNomCortoActual.GetAttribute("value");
            Assert.AreEqual(txtNomCortoExpected, txtNomCortoActualTexto);

            IWebElement txtPaisActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("codigoPais")));
            string txtPaisTexto = txtPaisActual.GetAttribute("value");
            Assert.AreEqual(txtPaisExpected, txtPaisTexto);
			//Regresar a lista de instituciones educativas
			IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarInstitucionEducativa"))); 
			btnCancelar.Click();
		}
            

        // ------------------------------------------------------ EDITAR
        [Test, Order(2)]
        public void Editar_Institucion_Educativa_Con_Informacion_Basica()
        {
            //Variables
            string txtNombreEditado = "Institución Educativa 1 editada";
            string txtNombreCortoEditado = "Nom Corto editada";
            string txtPaisEditado = "sv";

            //Editar Institución Educativa creada
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlInstitucionesEducativas_txtQuickSearch")));
            txtBuscar.SendKeys(txtNombreExpected);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlInstitucionesEducativas_grid']/div[3]/table/tbody/tr[1]/td[2]")));
            actions.DoubleClick(tblRegistro).Perform();

            IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
            txtNombre.Clear();
            txtNombre.SendKeys(txtNombreEditado);
            IWebElement txtCorto = driver.FindElement(By.Id("NombreCorto"));
            txtCorto.Clear();
            txtCorto.SendKeys(txtNombreCortoEditado);
            IWebElement paisEditado = driver.FindElement(By.Id("codigoPais"));
            paisEditado.Click();
            IWebElement paisSalvador = driver.FindElement(By.XPath("//*[@id='codigoPais']/option[6]"));
            paisSalvador.Click();
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarInstitucionEducativa")));
            btnGuardar.Click();

            //Editar la institución educativa editada
            txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlInstitucionesEducativas_txtQuickSearch")));
            txtBuscar.Clear();
            txtBuscar.SendKeys(txtNombreEditado);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            IWebElement tblRegistroEditado = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlInstitucionesEducativas_grid']/div[3]/table/tbody/tr[1]/td[2]")));
            tblRegistroEditado.Click();
            Thread.Sleep(1000);
            IWebElement btnEditar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlInstitucionesEducativas_edit']")));
            btnEditar.Click();

            //Verificaciones
            IWebElement txtNombreActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Nombre")));
            string txtNombreActualTexto = txtNombreActual.GetAttribute("value");
            Assert.AreEqual(txtNombreEditado, txtNombreActualTexto);

            IWebElement txtCortoActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("NombreCorto")));
            string txtCortoTexto = txtCortoActual.GetAttribute("value");
            Assert.AreEqual(txtNombreCortoEditado, txtCortoTexto);

            IWebElement txtPaisActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("codigoPais")));
            string txtPaisTexto = txtPaisActual.GetAttribute("value");
            Assert.AreEqual(txtPaisEditado, txtPaisTexto);
            //Regresar a lista de Instituciones Educativas
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarInstitucionEducativa"))); 
            btnCancelar.Click();
        }

        // ------------------------------------------------------ GUARDAR DATOS EN BLANCO
        [Test, Order(3)]
        public void Guardar_Institución_Educativa_Con_Campos_En_Blanco()
        {
            //Inicialización de variables
            var txtErrorExpected = "El nombre es requerido";

            //Crear nueva Institución Educativa
            IWebElement btnNuevo = driver.FindElement(By.Id("smlInstitucionesEducativas_new"));
            btnNuevo.Click();
            IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarInstitucionEducativa")));
            btnGuardar.Click();

            //Verificaciones
            IWebElement msgError = driver.FindElement(By.XPath("//*[@id='MessagesAndErrors']/div"));
            Assert.AreEqual(txtErrorExpected, msgError.Text);

            //Regresar a lista de Instituciones Educativas
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarInstitucionEducativa"))); 
            btnCancelar.Click();
        }

        // -------------------------------------------------------- CONSULTAR
        [Test, Order(4)]
        public void Consultar_Una_Institución_Educativa_Con_Informacion_Basica()
        {

            var txtdatos = "Institución Educativa 1 editada";
            var txtnombreCorto = "Nom Corto editada";
            var txtPais = "El Salvador";

            // Ingresar a usuario auditoria
        
            Logout();
            Thread.Sleep(2000);
            Login("auditoria", "auditoria");

            //Ir a pantalla de instituciones educativas
            IWebElement applicacion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menucenter']/a[2]")));
            applicacion.Click();
            IWebElement seccion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/table[4]/tbody/tr/td/div[1]/div[2]/h2/a")));
            seccion.Click();
            IWebElement opcion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset/div/div[3]/div[3]/div[2]/h2/a")));
            opcion.Click();

            // Buscar institucion educativa
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlInstitucionesEducativas_txtQuickSearch")));
            txtBuscar.SendKeys(txtdatos);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlInstitucionesEducativas_grid']/div[3]/table/tbody/tr[1]/td[2]")));
            actions.DoubleClick(tblRegistro).Perform();


            // Comparar datos
            IWebElement msgdatos = driver.FindElement(By.XPath("//*[@id='innerright']/div[2]/fieldset/div[2]/span"));
            Assert.AreEqual(txtdatos, msgdatos.Text);
            IWebElement msgCorto = driver.FindElement(By.XPath("//*[@id='innerright']/div[2]/fieldset/div[3]/span"));
            Assert.AreEqual(txtnombreCorto, msgCorto.Text);
            IWebElement msggrupo = driver.FindElement(By.XPath("//*[@id='innerright']/div[2]/fieldset/div[4]/span"));
            Assert.AreEqual(txtPais, msggrupo.Text);

            // Ingresar a usuario soporteit
            Logout();
            Login("soporteit", "soporteit");

        }

        //Eliminar una Institución Educativa

        [Test, Order(5)]
        public void Eliminar_Institución_Educativa()
        {
            //Inicialización de variables
            string txtNombreEditado = "Institución Educativa 1 editada";

            //Eliminar una Institución Educativa
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlInstitucionesEducativas_txtQuickSearch")));
            txtBuscar.SendKeys(txtNombreEditado);
            txtBuscar.SendKeys(Keys.Enter);
            //txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlInstitucionesEducativas_grid']/div[3]/table/tbody")));
            tblRegistro.Click();
            IWebElement btnEliminar = driver.FindElement(By.Id("smlInstitucionesEducativas_delete"));
            btnEliminar.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            //Verificaciones
            IWebElement lblRegistros = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/div[2]/div[2]/div[4]/span[2]")));
            Assert.AreEqual("No hay registros.", lblRegistros);
        }

        public void Login(string username, string password)
        {
            IWebElement inputUser = driver.FindElement(By.Id("txtUsername"));
            inputUser.SendKeys(username);
            IWebElement inputPassword = driver.FindElement(By.Id("txtPassword"));
            inputPassword.SendKeys(password);
            inputPassword.SendKeys(Keys.Enter);
        }
        public void Logout()
        {
            IWebElement CerrarSesion = driver.FindElement(By.XPath("//*[@id='topmenu']/a[1]"));
            CerrarSesion.Click();
        }


    }

}