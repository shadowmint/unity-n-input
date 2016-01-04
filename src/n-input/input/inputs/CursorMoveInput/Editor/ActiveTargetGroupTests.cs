#if N_INPUT_TESTS
using UnityEngine;
using N;
using N.Package.Input;
using System.Collections.Generic;
using NUnit.Framework;
using N.Package.Input.Impl.CursorMoveInput;
using System.Linq;

public class ActiveTargetGroupTests : N.Tests.Test
{
    [Test]
    public void test_active_group_reset()
    {
        var g1 = this.SpawnBlank();
        var g2 = this.SpawnBlank();

        var instance = new ActiveTargetGroup();
        Assert(instance.Active(g1));
        Assert(instance.Active(g2));

        Assert(instance.Inactive().ToList().Count == 0);

        instance.Reset();
        Assert(instance.Inactive().ToList().Count == 2);

        Assert(!instance.Active(g1));
        Assert(!instance.Active(g2));
        Assert(instance.Inactive().ToList().Count == 0);

        this.TearDown();
    }

    [Test]
    public void test_active_group_filter()
    {
        var g1 = this.SpawnBlank();
        var g2 = this.SpawnBlank();
        var g3 = this.SpawnBlank();

        var instance = new ActiveTargetGroup();

        // Round 1
        instance.Reset();
        Assert(instance.Active(g1));
        Assert(instance.Active(g2));
        Assert(instance.Active(g3));
        Assert(instance.Inactive().ToList().Count == 0);
        instance.FilterInactive();

        // Round 2; drop g3
        instance.Reset();
        Assert(!instance.Active(g1));
        Assert(!instance.Active(g2));
        Assert(instance.Inactive().ToList().Count == 1);
        instance.FilterInactive();

        // Round 2; add g3
        instance.Reset();
        Assert(!instance.Active(g1));
        Assert(!instance.Active(g2));
        Assert(instance.Active(g3));
        Assert(instance.Inactive().ToList().Count == 0);
        instance.FilterInactive();

        this.TearDown();
    }
}
#endif
