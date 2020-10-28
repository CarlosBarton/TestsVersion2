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
    public class AreasFuncionales
    {

        IWebDriver driver;
        WebDriverWait wait;
        Actions actions;
        string txtNombreExpected;

        [OneTimeSetUp]
        public void SetUp()
        {
            //Inicialización de variables
            CommonLogic commonLogic = new CommonLogic();
            driver = WebDriverSingleton.GetInstance();
            actions = new Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            txtNombreExpected = "Área Funcional 1";

            //Ir a pantalla de áreas funcionales
            IWebElement applicacion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menucenter']/a[2]")));
            applicacion.Click();
            IWebElement modulo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/table[1]/tbody/tr/td/div[1]/div[2]/h2/a")));
            modulo.Click();
            IWebElement seccion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/h4/a")));
            seccion.Click();
            IWebElement opcion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/div/div[1]/div[1]/div[2]/h2/a")));
            opcion.Click();
        }

        [Test, Order(1)]
        public void Crear_Area_Funcional_Con_Informacion_Basica()
        {
            //Crear nueva área funcional
            IWebElement btnNuevo = driver.FindElement(By.Id("smlAreasFuncionales_new"));
            btnNuevo.Click();
            IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
            txtNombre.SendKeys(txtNombreExpected);
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarAreaFuncional")));
            btnGuardar.Click();
            
            //Editar el área funcional creada
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlAreasFuncionales_txtQuickSearch")));
            txtBuscar.SendKeys(txtNombreExpected);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[3]/table/tbody/tr")));
            actions.DoubleClick(tblRegistro).Perform();

            //Verificaciones
            IWebElement txtNombreActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Nombre")));
            string txtNombreActualTexto = txtNombreActual.GetAttribute("value");
            Assert.AreEqual(txtNombreExpected, txtNombreActualTexto);

            //Regresar a lista de áreas funcionales
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarAreaFuncional")));;
            btnCancelar.Click();
        }

        // ------------------------------------------------------ EDITAR
        [Test, Order(2)]
        public void Editar_Area_Funcional_Con_Informacion_Basica()
        {
            //Variables
            string txtNombreEditado = "Área Funcional 1 editada";

            //Editar el área funcional creada
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlAreasFuncionales_txtQuickSearch")));
            txtBuscar.SendKeys(txtNombreExpected);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[3]/table/tbody/tr")));
            actions.DoubleClick(tblRegistro).Perform();

            IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
            txtNombre.Clear();
            txtNombre.SendKeys(txtNombreEditado);
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarAreaFuncional")));
            btnGuardar.Click();

            //Editar el área funcional editada
            txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlAreasFuncionales_txtQuickSearch")));
            txtBuscar.Clear();
            txtBuscar.SendKeys(txtNombreEditado);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            IWebElement tblRegistroEditado = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[3]/table/tbody/tr[1]/td[1]")));
            tblRegistroEditado.Click();
            Thread.Sleep(1000);
            IWebElement btnEditar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlAreasFuncionales_edit']")));
            btnEditar.Click();

            //Verificaciones
            IWebElement txtNombreActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Nombre")));
            string txtNombreActualTexto = txtNombreActual.GetAttribute("value");
            Assert.AreEqual(txtNombreEditado, txtNombreActualTexto);

            //Regresar a lista de áreas funcionales
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarAreaFuncional"))); ;
            btnCancelar.Click();
        }

        // ------------------------------------------------------ GUARDAR DATOS EN BLANCO
        [Test, Order(3)]
        public void Guardar_Area_Funcional_Con_Campos_En_Blanco()
        {
            //Inicialización de variables
            var txtErrorExpected = "Favor ingrese el nombre del área funcional";

            //Crear nueva área funcional
            IWebElement btnNuevo = driver.FindElement(By.Id("smlAreasFuncionales_new"));
            btnNuevo.Click();
            IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarAreaFuncional")));
            btnGuardar.Click();

            //Verificaciones
            IWebElement msgError = driver.FindElement(By.XPath("//*[@id='MessagesAndErrors']/div/ul/li"));
            Assert.AreEqual(txtErrorExpected, msgError.Text);

            //Regresar a lista de áreas funcionales
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarAreaFuncional"))); ;
            btnCancelar.Click();
        }

        // -------------------------------------------------------- CONSULTAR
        [Test, Order(4)]
        public void Consultar_Un_Area_Funcional_Con_Informacion_Basica()
        {

            var txtdatos = "Área Funcional 1 editada";
            var txtgrupo = "Aseinfo";

            // Ingresar a usuario auditoria
            Logout();
            Login("auditoria", "auditoria");

            //Ir a pantalla de áreas funcionales
            IWebElement applicacion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menucenter']/a[2]")));
            applicacion.Click();
            IWebElement modulo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/table[1]/tbody/tr/td/div[1]/div[2]/h2/a")));
            modulo.Click();
            IWebElement seccion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/h4/a")));
            seccion.Click();
            IWebElement opcion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/div/div[1]/div[1]/div[2]/h2/a")));
            opcion.Click();

            // Buscar área funcional
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlAreasFuncionales_txtQuickSearch")));
            txtBuscar.SendKeys(txtdatos);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[3]/table/tbody/tr")));
            actions.DoubleClick(tblRegistro).Perform();

            // Comparar datos

            IWebElement msgdatos = driver.FindElement(By.XPath("//*[@id='innerright']/div[2]/fieldset/div[2]/span"));
            Assert.AreEqual(txtdatos, msgdatos.Text);
            IWebElement msggrupo = driver.FindElement(By.XPath("//*[@id='innerright']/div[2]/fieldset/div[3]/span"));
            Assert.AreEqual(txtgrupo, msggrupo.Text);

            // Ingresar a usuario soporteit
            Logout();
            Login("soporteit", "soporteit");
        }

        //Eliminar un área funcional
        [Test, Order(5)]
        public void Eliminar_Area_Funcional()
        {
            //Inicialización de variables
            string txtNombreEditado = "Área Funcional 1 editada";

            //Eliminar un área funcional
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlAreasFuncionales_txtQuickSearch")));
            txtBuscar.SendKeys(txtNombreEditado);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[3]/table/tbody/tr")));
            tblRegistro.Click();
            IWebElement btnEliminar = driver.FindElement(By.Id("smlAreasFuncionales_delete"));
            btnEliminar.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            //Verificaciones
            IWebElement lblRegistros = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[4]/span[2]")));
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