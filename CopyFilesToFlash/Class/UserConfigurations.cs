﻿using CopyFilesToFlash.Models;
using CopyFilesToFlash.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CopyFilesToFlash;

public class UserConfigurations : ConfigurationSection
{
    public UserConfigurations()
    {

    }

    [ConfigurationProperty(name: "VID", DefaultValue = null)]
    public string VID
    {
        get { return (string)this["VID"]; }
        set { this["VID"] = value;}
    }

    [ConfigurationProperty(name: "PID", DefaultValue = null)]
    public string PID
    {
        get { return (string)this["PID"]; }
        set { this["PID"] = value; }
    }

    [ConfigurationProperty(name: "Format", DefaultValue = false)]
    public bool Format
    {
        get { return (bool)this["Format"]; }
        set { this["Format"] = value;}
    }

    [ConfigurationProperty(name: "Eject", DefaultValue = false)]
    public bool Eject
    {
        get { return (bool)this["Eject"]; }
        set { this["Eject"] = value;}
    }


    [ConfigurationProperty(name: "Files", DefaultValue = null)]
    public string Files
    {
        get { return (string)this["Files"]; }
        set { this["Files"] = value; }
    }

    public void SetFiles(ObservableCollection<FileToCopy> files)
    {
        if (files != null )
        {
            if (files.Count==0)
            {
                Files = "";
                return;

            }
            string strFiles = string.Empty;
            foreach (FileToCopy itemFile in files)
            {
                strFiles += itemFile.FileName + "\r\n";
                strFiles += itemFile.FilePath + "\r\n";
            }
            Files = strFiles.Remove(strFiles.Length - 2, 2);
        }
    }

    public ObservableCollection<FileToCopy> GetFiles(MainViewModel mainViewModel)
    {
        ObservableCollection<FileToCopy> bResponse = [];
        if (string.IsNullOrEmpty(Files)) return bResponse;

        string[] bResult = Files.Split("\r\n");
        if (bResult!=null &&  bResult.Length > 0)
        {
            for (int i=0;i< bResult.Length;i++)
            {
                FileToCopy itemFile = new(mainViewModel);
                itemFile.FileName = bResult[i];
                i++;
                itemFile.FilePath = bResult[i];
                bResponse.Add(itemFile);
            }
        }
        int memberIndex = 1;
        foreach (FileToCopy itemFile in bResponse)
        {
            itemFile.FileIndex = memberIndex;
            memberIndex++;
        }
        return bResponse;
    }
}
