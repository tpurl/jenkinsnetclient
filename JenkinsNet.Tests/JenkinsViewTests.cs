﻿namespace JenkinsNet.Tests
{
    using System.Collections.Generic;
    using System.Configuration;
    using JenkinsNet;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class JenkinsViewTests
    {
        private JenkinsConnection jenkinsConnection = new JenkinsConnection(ConfigurationManager.AppSettings["JenkinsURL"]);

        [TestMethod]
        public void JenkinsView_Create_Exists_Delete()
        {
            var newView = new JenkinsView(this.jenkinsConnection, "hudson.model.ListView", "JenkinsView_Create_Exists_Delete");

            Assert.IsTrue(newView.Create());
            Assert.IsTrue(newView.Create());
            Assert.IsFalse(newView.Create(true));

            Assert.IsTrue(newView.Exists());

            Assert.IsTrue(newView.Delete());
            Assert.IsTrue(newView.Delete());
            Assert.IsFalse(newView.Delete(true));

            Assert.IsFalse(newView.Exists());
        }

        [TestMethod]
        public void JenkinsView_Create_AddJob_Contains_RemoveJob_Delete()
        {
            var newView = new JenkinsView(this.jenkinsConnection, "hudson.model.ListView", "JenkinsView_Create_AddJob_RemoveJob_Delete");
            var job = new JenkinsJob(this.jenkinsConnection, "hudson.model.FreeStyleProject", "LimsRestAPI-clone");

            Assert.IsTrue(newView.Create());

            Assert.IsTrue(newView.Add(job));
            Assert.IsTrue(newView.Add(job));
            Assert.IsFalse(newView.Add(job, true));

            Assert.IsTrue(newView.Contains(job));

            Assert.IsTrue(newView.Remove(job));
            Assert.IsTrue(newView.Remove(job));
            Assert.IsFalse(newView.Remove(job, true));

            Assert.IsFalse(newView.Contains(job));

            Assert.IsTrue(newView.Delete());
        }
    }
}
