using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class FileReaderInstaller : MonoInstaller
{
    [SerializeField] private FileReader _fileReader;


    public override void InstallBindings()
    {
        BindGraphPoints();
    }

    private void BindGraphPoints()
    {
        var points = _fileReader.DesirializeOilTracks();

        Container.Bind<List<float>>().
            WithId("Points").
            FromInstance(points).
            AsCached().
            NonLazy();
    }
}