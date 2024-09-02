using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BraketsEngine;
using Microsoft.Xna.Framework;

namespace BraketsEngine;

public class ParticleEmitterData
{
    public ParticleData particleData = new();

    public float angle = 0f;
    public float angleVariance = 45f;

    public float lifeSpanMin = 0.1f;
    public float lifeSpanMax = 2f;

    public float speedMin = 100f;
    public float speedMax = 100f;

    public float sizeStartMin = 1;
    public float sizeStartMax = 4;
    public float sizeEndMin = 24;
    public float sizeEndMax = 36;

    public Color colorStart = Color.Yellow;
    public Color colorEnd = Color.Red;

    public float opacityStart = 1f;
    public float opacityEnd = 0f;

    public float interval = 1f;
    public int emitCount = 1;

    public bool visible = true;

    public string textureName = "builtin/particle_default";

    public ParticleEmitterData() { }

    public ParticleEmitterData FromFile(string filename)
    {
        string path = $"{Path.Combine(Globals.CurrentDir, "content/", "particles", filename)}.particles";
        if (!Path.Exists(path))
        {
            Debug.Error($"ParticleEmitterData with filename {filename} not found!", this);

            return new ParticleEmitterData();
        }

        string[] data = File.ReadAllLines(path);
        foreach (var line in data)
        {
            if (line == string.Empty)
                continue;

            string key = line.Split(":")[0];
            string value = line.Split(":")[1];
            switch (key)
            {
                case "angleVariance":
                    angleVariance = float.Parse(value);
                    break;
                case "lifeSpanMin":
                    lifeSpanMin = float.Parse(value);
                    break;
                case "lifeSpanMax":
                    lifeSpanMax = float.Parse(value);
                    break;
                case "speedMin":
                    speedMin = float.Parse(value);
                    break;
                case "speedMax":
                    speedMax = float.Parse(value);
                    break;
                case "sizeStartMin":
                    sizeStartMin = float.Parse(value);
                    break;
                case "sizeStartMax":
                    sizeStartMax = float.Parse(value);
                    break;
                case "sizeEndMin":
                    sizeEndMin = float.Parse(value);
                    break;
                case "sizeEndMax":
                    sizeEndMax = float.Parse(value);
                    break;
                case "colorStart":
                    colorStart = new Color(VecParser.ParseVec4(value));
                    break;
                case "colorEnd":
                    colorEnd = new Color(VecParser.ParseVec4(value));
                    break;
                case "opacityStart":
                    opacityStart = float.Parse(value);
                    break;
                case "opacityEnd":
                    opacityEnd = float.Parse(value);
                    break;
                case "interval":
                    interval = float.Parse(value);
                    break;
                case "emitCount":
                    emitCount = int.Parse(value);
                    break;
                case "visible":
                    visible = bool.Parse(value);
                    break;
                case "textureName":
                    textureName = value;
                    break;
            }
        }

        return this;
    }
}

public class ParticleEmitter
{
    public string Name;
    public Vector2 Position;
    public int Layer;

    internal bool drawOnLoading = false;
    
    private readonly ParticleEmitterData _emitterData;
    private float _intervalLeft;

    List<Particle> particles = new List<Particle>();
    private bool enabled = true;

    public ParticleEmitter(string name, Vector2 pos, ParticleEmitterData particleEmitterData, int layer, bool drawOnLoading = false)
    {
        this.Name = name;
        this.Position = pos;
        this.Layer = layer;
        this.enabled = true;
        this.drawOnLoading = drawOnLoading;

        _emitterData = particleEmitterData;
        _intervalLeft = particleEmitterData.interval;

        ParticleManager.AddParticleEmitter(this);
    }

    private void Emit()
    {
        ParticleData particleData = _emitterData.particleData;
        particleData.sizeStart = Randomize.FloatInRange(_emitterData.sizeStartMin, _emitterData.sizeStartMax);
        particleData.sizeEnd = Randomize.FloatInRange(_emitterData.sizeEndMin, _emitterData.sizeEndMax);
        particleData.lifeSpan = Randomize.FloatInRange(_emitterData.lifeSpanMin, _emitterData.lifeSpanMax);
        particleData.speed = Randomize.FloatInRange(_emitterData.speedMin, _emitterData.speedMax);
        particleData.colorStart = _emitterData.colorStart;
        particleData.colorEnd = _emitterData.colorEnd;
        particleData.opacityStart = _emitterData.opacityStart;
        particleData.opacityEnd = _emitterData.opacityEnd;
        particleData.textureName = _emitterData.textureName;

        float r = (float)(new Random().NextDouble() * 2) - 1;
        particleData.angle = _emitterData.angleVariance * r;

        Particle p = new Particle(Position, particleData, this.Layer);
        p.drawOnLoading = drawOnLoading;
        particles.Add(p);
    }

    public void SetVisible(bool value)
    {
        _emitterData.visible = value;
    }

    public void SetEnable(bool value)
    {
        this.enabled = value;
    }
    public async void Burst(int burstTime)
    {
        if (enabled) SetVisible(true);
        await Task.Delay(burstTime);
        SetVisible(false);
    }

    public void ModifyParticleDataProp(string fieldName, object value)
    {
        Type emitterDataType = _emitterData.GetType();
        FieldInfo field = emitterDataType.GetField(fieldName);

        if (field != null)
        {
            object convertedValue = Convert.ChangeType(value, field.FieldType);
            field.SetValue(_emitterData, convertedValue);
        }
        else
        {
            Debug.Warning($"Field '{fieldName}' of {_emitterData} not found");
        }
    }


    public void Unload()
    {
        foreach (var p in particles.ToList())
        {
            p.DestroySelf();
            particles.Remove(p);
        }
    }

    public void Update()
    {
        _intervalLeft -= Globals.DEBUG_DT;
        while (_intervalLeft <= 0f)
        {
            _intervalLeft += _emitterData.interval;
            for (int i = 0; i < _emitterData.emitCount; i++)
            {
                if (_emitterData.visible)
                    Emit();
            }
        }
    }
}